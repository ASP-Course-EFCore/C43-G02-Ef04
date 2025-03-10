using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Models
{
    class Department
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public DateOnly DateOfCreation { get; set; }
        public int Serial { get; set; }

        [ForeignKey(nameof(Manager))]
        public int? DeptManagerId { get; set; }//Optional [Allow null when inserting data]

        [InverseProperty(nameof(Employee.ManagedDepartment))]
        public Employee Manager { get; set; } = null!;

        [InverseProperty(nameof(Employee.EmployeeDepartment))]
        public ICollection<Employee> Employees { get; set; } = null!;
                                                                     
    }
}
