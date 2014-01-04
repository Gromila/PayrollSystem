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
    /// Interaction logic for SetWorkedHoursWindow.xaml
    /// </summary>
    public partial class SetWorkedHoursWindow : Window
    {
        public Employee Employee { get; set; }

        public SetWorkedHoursWindow()
        {
            InitializeComponent();
        }

        private void SetHoursClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (WorkedHoursTextBox.Text == String.Empty)
                    throw new Exception("Не заполнено поле!");
                Employee.WorkedHours = int.Parse(WorkedHoursTextBox.Text);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
    }
}
