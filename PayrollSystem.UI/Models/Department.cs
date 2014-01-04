using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PayrollSystem.UI.Models
{
    [Serializable]
    public class Department
    {
        public String Name { get; set; }

        public double Rate { get; set; }

        [XmlArray("Employees"), XmlArrayItem("Employee")]
        public EmployeesCollection Employees { get; set; }
    }
}
