using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Microsoft.Win32;
using PayrollSystem.UI.Models;

namespace PayrollSystem.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Создаём новый объект дабы потом юзать его    
            App.Departments = new DepartmentsCollection();
            // Для левого листбокса указываем источник
            DepartmentsListBox.ItemsSource = App.Departments;
        }

        /// <summary>
        /// При изменении выбранного элемента в левом листбоксе, обновляем таблицу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var department = (Department) DepartmentsListBox.SelectedItem;
            if (department != null)
                EmployeesDataGrid.ItemsSource = department.Employees; // источник таблицы - сотрудники выбранного отдела
        }
        /// <summary>
        /// Вызов окна добавления отдела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDepartment(object sender, RoutedEventArgs e)
        {
            var addDepartment = new AddDepartmentWindow();
            addDepartment.ShowDialog();
        }

        /// <summary>
        /// Вызов окна удаления отдела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteDepartment(object sender, RoutedEventArgs e)
        {
            var deleteDepartment = new DeleteDepartmentWindow();
            deleteDepartment.ShowDialog();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// При выходе из приложения запрашиваем подтверждение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Вызов окна добавления работника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            var addEmployee = new AddEmployeeWindow();
            addEmployee.ShowDialog();
        }

        /// <summary>
        /// Вызов окна удаления работника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            var deleteEmployee = new DeleteEmployeeWindow();
            deleteEmployee.ShowDialog();
        }

        /// <summary>
        /// Вызов окна изменения выработанных часов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetWorkedHours(object sender, RoutedEventArgs e)
        {
            var setWorkedHoursWindow = new SetWorkedHoursWindow {Employee = (Employee) EmployeesDataGrid.SelectedItem};
            setWorkedHoursWindow.ShowDialog();
            UpdatePayTextBox();
        }


        /// <summary>
        /// Вызов окна изменения отдела выбранного сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeDepartment(object sender, RoutedEventArgs e)
        {
            var changeDepartment = new ChangeDepartmentWindow();
            changeDepartment.CurrentDepartment = (Department)DepartmentsListBox.SelectedItem;
            changeDepartment.CurrentEmployee = (Employee) EmployeesDataGrid.SelectedItem;
            changeDepartment.ShowDialog();
        }

        /// <summary>
        /// Вызов окна изменения оклада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeSalary(object sender, RoutedEventArgs e)
        {
            var changeSalary = new ChangeSalaryWindow();
            changeSalary.CurrentEmployee = ((Employee) EmployeesDataGrid.SelectedItem);
            changeSalary.ShowDialog();
            UpdatePayTextBox();
        }

        /// <summary>
        /// При изменении выбранной строки таблицы (сотрудника) обновляем кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmployeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeesDataGrid.SelectedItem != null)
            {
                PayButton.IsEnabled = true;
                SalaryButton.IsEnabled = true;
                DepartmentButton.IsEnabled = true;
                HoursButton.IsEnabled = true;
            }
            else
            {
                PayButton.IsEnabled = false;
                SalaryButton.IsEnabled = false;
                DepartmentButton.IsEnabled = false;
                HoursButton.IsEnabled = false;
            }
            UpdatePayTextBox();
        }

        /// <summary>
        /// Пересчитываем сумму к выплате для выбранного работника
        /// </summary>
        private void UpdatePayTextBox()
        {
            var employee = (Employee)EmployeesDataGrid.SelectedItem;
            if (employee != null)
            {
                var department = (Department)DepartmentsListBox.SelectedItem;
                PayTextBox.Text = (department.Rate * employee.Salary * employee.WorkedHours / employee.NormHours).ToString();
            }
        }

        /// <summary>
        /// Делаем выплату
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pay(object sender, RoutedEventArgs e)
        {
            ((Employee) EmployeesDataGrid.SelectedItem).WorkedHours = 0;
            UpdatePayTextBox();
        }

        /// <summary>
        /// Сериализуем всю инфу в xml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveStorage(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Xml (*.xml)|*.xml";
            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    var serializer = new XmlSerializer(typeof(DepartmentsCollection));
                    using (var stream = File.Open(saveFileDialog.FileName, FileMode.CreateNew))
                    {
                        serializer.Serialize(stream, App.Departments);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        /// <summary>
        /// Десериализуем всю инфу из файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenStorage(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Xml (*.xml)|*.xml";
            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    var serializer = new XmlSerializer(typeof(DepartmentsCollection));
                    using (var stream = File.OpenRead(openFileDialog.FileName))
                    {
                        App.Departments = (DepartmentsCollection) serializer.Deserialize(stream);
                        DepartmentsListBox.ItemsSource = App.Departments;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
    }
}
