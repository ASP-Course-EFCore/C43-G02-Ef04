using Demo.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.ModelsConfigurations
{
    class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> department)
        {

            department.ToTable("Departments", "Sales");

            department.HasKey(D => D.DeptId);
            department.Property(D => D.DeptId)
                      .UseIdentityColumn(10, 10);

            department.Property(D => D.Name)
                      .HasColumnName("DepartmentName")
                      .HasColumnType("varchar")
                      .HasMaxLength(20)
                      .IsRequired(false)
                      .HasDefaultValue("HR");

            department.Property(D => D.DateOfCreation)
                      .HasAnnotation("DataType", "Date")
                      .HasDefaultValueSql("GetDate()");

            department.Ignore(D => D.Serial);

            department.HasOne<Employee>(D => D.Manager)
                      .WithOne(E => E.ManagedDepartment)
                      .HasForeignKey<Department>(D => D.DeptManagerId)
                      .IsRequired(false);


        }
    }
}
