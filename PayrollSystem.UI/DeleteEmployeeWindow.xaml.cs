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
    /// Interaction logic for DeleteEmployeeWindow.xaml
    /// </summary>
    public partial class DeleteEmployeeWindow : Window
    {
        public DeleteEmployeeWindow()
        {
            InitializeComponent();

            // источник для списка
            DepartmentsComboBox.ItemsSource = App.Departments;
            // пока не выбран отдел, нельзя выбирать работника
            EmployeesComboBox.IsEnabled = false;
        }

        private void DepartmentsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartmentsComboBox.SelectedItem != null)
            {
                // задали источник для списка работников
                EmployeesComboBox.ItemsSource = ((Department) DepartmentsComboBox.SelectedItem).Employees;
                EmployeesComboBox.IsEnabled = true;
            }
        }

        private void DeleteEmployeeClick(object sender, RoutedEventArgs e)
        {
            if (
                MessageBox.Show(
                    "Вы уверены, что хотите уволить " + ((Employee) EmployeesComboBox.SelectedItem).Name + "?",
                    "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                for (int i = 0; i < App.Departments.Count; i++)
                {
                    if (App.Departments[i].Name == ((Department) DepartmentsComboBox.SelectedItem).Name)
                    {
                        App.Departments[i].Employees.Remove((Employee) EmployeesComboBox.SelectedItem);
                    }
                }
            }
            this.Close();
        }
    }
}
