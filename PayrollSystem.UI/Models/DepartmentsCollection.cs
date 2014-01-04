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
    /// ObservableCollection - класс с набором событий, которые активируются при изменении содержимого объекта
    /// Используется для обновления ListBox и таблицы при добавлении/удалении элементов
    /// </summary>
    public class DepartmentsCollection : ObservableCollection<Department>
    {
        public void CopyFrom(IEnumerable<Department> departments)
        {
            Items.Clear();
            foreach (var item in departments)
            {
                Items.Add(item);
            }
            // событие, вызываемое при изменении всего списка
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
