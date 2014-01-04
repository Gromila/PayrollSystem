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
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        public AddEmployeeWindow()
        {
            InitializeComponent();

            DepartmentsComboBox.ItemsSource = App.Departments;
        }

        private void AddEmployeeClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // проверяем поля
                if (NameTextBox.Text == String.Empty || PlaceTextBox.Text == String.Empty ||
                    SalaryTextBox.Text == String.Empty || PersonalNumberTextBox.Text == String.Empty ||
                    NormHoursTextBox.Text == String.Empty || DepartmentsComboBox.SelectedItem == null)
                {
                    throw new Exception("Не все поля заполнены!");
                }
                var employee = new Employee()
                {
                    Name = NameTextBox.Text,
                    Place = PlaceTextBox.Text,
                    // int.Parse - преобразует строку в число
                    Salary = int.Parse(SalaryTextBox.Text),
                    PersonalNumber = int.Parse(PersonalNumberTextBox.Text),
                    NormHours = int.Parse(NormHoursTextBox.Text),
                    WorkedHours = 0
                };
                // Ищем в нашем списке нужный отдел
                for (int i = 0; i < App.Departments.Count; i++)
                {
                    if (App.Departments[i].Name == ((Department) DepartmentsComboBox.SelectedItem).Name)
                    {
                        // добавляем нового работника
                        App.Departments[i].Employees.Add(employee);
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
    }
}
