using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PayrollSystem.UI.Models;

namespace PayrollSystem.UI
{
    /// <summary>
    /// Interaction logic for ChangeDepartmentWindow.xaml
    /// </summary>
    public partial class ChangeDepartmentWindow : Window
    {
        public Department CurrentDepartment { get; set; }

        public Employee CurrentEmployee { get; set; }

        public ChangeDepartmentWindow()
        {
            InitializeComponent();
            // Источник данных для списка
            DepartmentComboBox.ItemsSource = App.Departments;
        }

        private void ChangeDepartmentClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // проверяем поля
                if (DepartmentComboBox.SelectedItem == null)
                    throw new Exception("Не все поля заполнены");
                // удаляем сотрудника из старого отдела
                CurrentDepartment.Employees.Remove(CurrentEmployee);

                // добавляем сотрудника в новый отдел
                ((Department)DepartmentComboBox.SelectedItem).Employees.Add(CurrentEmployee);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // когда окно загрузилось - обновляем текст в поле "Текущий отдел"
            CurrentDepartmentTextBox.Text = CurrentDepartment.Name;
        }
    }
}
