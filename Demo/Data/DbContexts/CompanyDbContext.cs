using Demo.Data.Models;
using Demo.Data.ModelsConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.DbContexts
{
    class CompanyDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .; Database = IslamCompany; Trusted_Connection = true; Encrypt = True; TrustServerCertificate = True");//Trust App To connect on sql server service throw Windows authentication.
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());
            modelBuilder.ApplyConfiguration<Employee>(new EmployeeConfigurations());

            ///Configure the relationship between [Employee-Department] in case you represent the navigational properties in the 2 classes
            ///RelationShip [Manage] Configuration between Employee-Department Classes.
            //modelBuilder.Entity<Employee>()
            //            .HasOne<Department>(E => E.ManagedDepartment)//Specify the Navigational property "ManagedDepartment" inside "Employee" Class
            //            .WithOne(D => D.Manager)//Specify the Navigational property "Manager" inside "Department" Class
            //            .HasForeignKey<Department>(D => D.DeptManagerId)//Specify the "FK" "DeptManagerId" inside "Department" class.
            //            .OnDelete(DeleteBehavior.NoAction)//To Change the default behavior from "Cascade" to "NoAction".
            //            .IsRequired(true);//Make the column of relationship required.

            ///To insert data in department table with migrate of table department.
            ///
            //modelBuilder.Entity<Department>().HasData
            //    (
            //        new Department() { DeptId = 70, DeptName = "Design", DateOfCreation = new DateOnly(2024, 12, 13) },
            //        new Department() { DeptId = 80, DeptName = "Software", DateOfCreation = new DateOnly(2024, 1, 13) }
            //    );
        }
    }
}
