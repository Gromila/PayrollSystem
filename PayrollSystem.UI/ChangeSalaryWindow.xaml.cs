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
    /// Interaction logic for ChangeSalaryWindow.xaml
    /// </summary>
    public partial class ChangeSalaryWindow : Window
    {
        public Employee CurrentEmployee { get; set; }

        public ChangeSalaryWindow()
        {
            InitializeComponent();
        }

        private void ChangeSalaryClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // проверили поля
                if (SalaryTextBox.Text == String.Empty)
                {
                    throw new Exception("Не все поля заполнены");
                }
                // изменили запрплату
                CurrentEmployee.Salary = int.Parse(SalaryTextBox.Text);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
    }
}
