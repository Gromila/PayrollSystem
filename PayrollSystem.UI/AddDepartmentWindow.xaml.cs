using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for AddDepartmentWindow.xaml
    /// </summary>
    public partial class AddDepartmentWindow : Window
    {
        public AddDepartmentWindow()
        {
            InitializeComponent();
        }

        private void AddDepartmentClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем поля
                if (DepartmentNameTextBox.Text == String.Empty || DepartmentRateTextBox.Text == String.Empty)
                    throw new Exception("Не все поля заполнены!");
                var department = new Department()
                {
                    Name = DepartmentNameTextBox.Text,
                    // Строка -> Вещественное число
                    Rate = double.Parse(DepartmentRateTextBox.Text, CultureInfo.InvariantCulture)
                };
                App.Departments.Add(department);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
    }
}
