using Demo.Data.DbContexts;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.Data_Seeding
{
    static class CompanyDbContextSeed
    {
        public static bool DataSeeding(CompanyDbContext dbContext)
        {
            try
            {
                if (!dbContext.Employees.Any())
                {
                    var EmployeesData = File.ReadAllText("DataSeeding\\employees.json");//Return Employees as string.
                    var Employees = JsonSerializer.Deserialize<List<Employee>>(EmployeesData);//Deserialize - Convert this string to objects.

                    if (Employees?.Count > 0)
                    {
                        dbContext.Employees.AddRange(Employees);
                    }
                    dbContext.SaveChanges();//Insert The Employees objects/Rows in the DB.
                }
                return true;//If Employee Table Has Data Already or Seeding done.
            }
            catch
            {
                return false;
            }
        }

        public static void Seeding(CompanyDbContext dbContext)
        {

            if (!dbContext.Departments.Any())
            {
                var departmentsData = File.ReadAllText("DataSeeding\\departments.json");
                var departments = JsonSerializer.Deserialize<List<Department>>(departmentsData);

                if (departments?.Count > 0)
                {
                    dbContext.AddRange(departments);
                }
                dbContext.SaveChanges();

            } 
            if (!dbContext.Employees.Any())
            {
                var employeesData = File.ReadAllText("DataSeeding\\employees.json");
                var employees = JsonSerializer.Deserialize<List<Employee>>(employeesData);

                if (employees?.Count > 0)
                {
                    dbContext.AddRange(employees);
                }
                dbContext.SaveChanges();

            } 
        }
    }
}
