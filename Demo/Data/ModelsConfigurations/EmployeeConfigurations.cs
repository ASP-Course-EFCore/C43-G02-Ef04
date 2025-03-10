using Demo.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.ModelsConfigurations
{
    class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> employee)
        {
            //Map The [Has] relationship between Employee - Department
            employee.OwnsOne<Address>(E => E.EmpAddress, Address => Address.WithOwner());

            //Map The [Works] relationship between Employee - Department
            employee.HasOne<Department>(E => E.EmployeeDepartment)//Each Employee Must Belong to one Department
                    .WithMany(D => D.Employees)//Each department must has multiple employees
                    .HasForeignKey(E => E.DepartmentId)
                    .IsRequired(true)//Make The Relationship Required - This DepartmentId can't be null.
                                 //So the relation ship is on delete [cascade]
                                 //Because there is no employee without DepartmentId
                                 //So In case The DepartmentId Deleted, the Employee Must deleted.
                    .OnDelete(DeleteBehavior.NoAction);

            ///If You Don't represent the Navigational properties in the 2 classes use this =>
            ///
            //employee.HasOne<Department>()
            //        .WithMany();
        }
    }
}
