using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.UI.Models
{
    /// <summary>
    /// Аналогично как для DepartmentCollection
    /// </summary>
    public class EmployeesCollection : ObservableCollection<Employee>
    {
        public void CopyFrom(IEnumerable<Employee> employees)
        {
            Items.Clear();
            foreach (var employee in employees)
            {
                Items.Add(employee);
            }
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
