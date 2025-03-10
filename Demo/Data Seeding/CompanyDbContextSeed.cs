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

                if (!dbContext.Employees.Any() || !dbContext.Departments.Any())
                {
                var departments = File.ReadAllText("DataSeeding\\departments.json");
                var Employees = File.ReadAllText("DataSeeding\\employees.json");

                var EEmployees = JsonSerializer.Deserialize<List<Employee>>(Employees);
                var ddepartments = JsonSerializer.Deserialize<List<Department>>(departments);

                if(EEmployees?.Count>0 && ddepartments?.Count > 0)
                {
                    dbContext.Departments.AddRange(ddepartments);
                    dbContext.Employees.AddRange(EEmployees);
                }
                dbContext.SaveChanges();
                }

        }
    }
}
