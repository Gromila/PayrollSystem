using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PayrollSystem.UI.Annotations;

namespace PayrollSystem.UI.Models
{
    [Serializable]
    public class Employee : INotifyPropertyChanged
    {
        #region Private Fields
        private int _workedHours;

        private String _place;

        private int _salary;
        #endregion
        public int PersonalNumber { get; set; }

        public String Name { get; set; }

        public String Place
        {
            get { return _place; }
            set { SetProperty(ref _place, value, "Place");}
        }

        public int Salary { 
            get { return _salary; }
            set { SetProperty(ref _salary, value, "Salary");}
        }

        public int WorkedHours
        {
            get { return _workedHours; }
            set { SetProperty(ref _workedHours, value, "WorkedHours");}
        }

        public int NormHours { get; set; }

        // про вот эту херню читаем в приложенном txt документике
        public event PropertyChangedEventHandler PropertyChanged;

        private void SetProperty<T>(ref T field, T value, String name)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                var handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(name));
                }
            }
        }
    }
}
