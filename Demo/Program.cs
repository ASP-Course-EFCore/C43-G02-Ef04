using Demo.Data.DbContexts;
using Demo.Data.Models;
using Demo.Data_Seeding;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace Demo
{
  
    internal class Program
    {
        //I need this code run one time not every time App Run
        //so put it inside this Constructor overload of class Program that run one time/Executed one time with first use of the program/App.
        static Program()
        {
            ///03.Dynamic Data Seeding
            ///
            ///
            ///Read The objects/data [Departments objects] from external file.
            ///To use dynamic data seeding
            ///We need function that called/executed once application run.
            ///We Can make this function inside the CompanyDbContext Class
            ///Or make new class has this function that take parameter of type CompanyDbContext.
            ///Every DbContext has Function to make seed.
            ///This Function DataSeeding() will try to read the data inside the file
            ///If it do it return true.
            ///If not return false.
            ///I make seed only if the table not has data.
            ///After make this function
            ///I need to put it in place that first called when APP run which is "Main()"
            using CompanyDbContext context = new CompanyDbContext();
            context.Database.Migrate();//To Apply Any pending migration and at it to DB.
            CompanyDbContextSeed.Seeding(context);//Seed for data.
        }
        static void Main(string[] args)
        {
            //using CompanyDbContext context = new CompanyDbContext();
            #region Part 03 Data Seeding

            ///It's a way to populate your database with initial data
            ///when the database is first created or migrated
            ///Like if you work on Development Stage, You need data to test your APP using it. 
            ///So i need when create the database or with first migration - To put data in DB [Data Seeding].

            ///There Are 3 ways for seeding the data => 
            /// 1.Manual  2.Using Migration  3.Dynamic Data Seeding

            ///01.Manual
            ///

            #region 01.1 - Add Only One Department

            //Department department01 = new Department()
            //{
            //    DeptName = "HR",
            //    DateOfCreation = new DateOnly(2024, 7, 25)
            //};
            //context.Departments.Add(department01);
            //context.SaveChanges(); 

            #endregion

            #region 01.1 - Add Many Departments using row constructor & AddRange().

            //List<Department> departments = new List<Department>()
            //{
            //    new Department(){DeptName = "IT", DateOfCreation = new DateOnly(2024,5,23)},
            //    new Department(){DeptName = "Development", DateOfCreation = new DateOnly(2025,1,23)},
            //};
            //context.Departments.AddRange(departments);
            //context.SaveChanges();

            #endregion


            ///02.Using Migration
            ///     -By using Function [HasData()] inside the function OnModelCreating() in the DbContext Class
            ///     -When Add Data using this way 
            ///     -You must specify value for any identity column like Id otherwise it will throw error.
            ///     -This Data Will Added only when make migration and update database because this data is in the OnModelCreating().

            #region Example
            //modelBuilder.Entity<Department>().HasData
            //(
            //    new Department() { DeptId = 70, DeptName = "Design", DateOfCreation = new DateOnly(2024, 12, 13) },
            //    new Department() { DeptId = 80, DeptName = "Software", DateOfCreation = new DateOnly(2024, 1, 13) }
            //); 
            #endregion


            ///03.Dynamic Data Seeding
            ///
            ///
            ///Read The objects/data [Departments objects] from external file.
            ///To use dynamic data seeding
            ///We need function that called/executed once application run.
            ///We Can make this function inside the CompanyDbContext Class
            ///Or make new class has this function that take parameter of type CompanyDbContext.
            ///Every DbContext has Function to make seed.
            ///This Function DataSeeding() will try to read the data inside the file
            ///If it do it return true.
            ///If not return false.
            ///I make seed only if the table not has data.
            ///After make this function
            ///I need to put it in place that first called when APP run which is "Main()"

            //bool flag = CompanyDbContextSeed.DataSeeding(context);

            //if (flag)
            //    Console.WriteLine("Seeding Done");
            //else
            //    Console.WriteLine("Seeding UnFinished");

            #endregion
        }
    }
}
