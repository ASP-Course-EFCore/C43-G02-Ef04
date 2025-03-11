using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Demo.Data.Models
{
    class Department
    {
        public int DeptId { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfCreation { get; set; }
        public int Serial { get; set; }

        [ForeignKey(nameof(Manager))]
        public int? DeptManagerId { get; set; }//Optional [Allow null when inserting data]

        [InverseProperty(nameof(Employee.ManagedDepartment))]
        public virtual Employee Manager { get; set; } = null!;

        [InverseProperty(nameof(Employee.EmployeeDepartment))]
        public virtual ICollection<Employee> Employees { get; set; } = null!;
                                                                     
    }
}
