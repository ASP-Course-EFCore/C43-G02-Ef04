using Demo.Data.DbContexts;
using Demo.Data.Models;
using Demo.Data_Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Net.Http.Headers;

namespace Demo
{

    internal class Program
    {
        ///I need this code run one time not every time App Run
        ///so put it inside this Constructor overload of class Program that run one time/Executed one time with first use of the program/App.
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
            using CompanyDbContext context = new CompanyDbContext();
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

            #region Part 04 Loading Related Data - Eager Loading

            //Like if you need to return the Employee Data based on his department
            //There is One-Many Relationship between Employee-Department
            //Employee Attach to One Department And One Department has many Employee
            //And there is Navigational Property "Employees" of Type ICollection<Employee> in Department Table
            //And there is Navigational Property "EmployeeDepartment" of Type Department in Employee Table

            #region Example01 - Return Data Of Employee With Code==2 & Department Name that he's related to [Using The Default Behavior - Navigational property not loaded]

            //var Emp01 = context.Employees.FirstOrDefault(E => E.Code == 2);

            //if (Emp01 is not null)
            //{
            //    Console.WriteLine($"Name:{Emp01.EmpName}");// Name: Sama
            //    Console.WriteLine($"DepartmentID:{Emp01.DepartmentId}");// DepartmentID:90
            //    Console.WriteLine($"Department Name:{Emp01.EmployeeDepartment?.Name}");// Department Name:
            //    //"Emp01.EmployeeDepartment?.Name" - Related Data Not Date of entry "Employee", it's data of entry "Department".
            //    //By Default - The Navigational property[Related Data] not loaded
            //    //So Before you print the Name of department - you need to load the data of this Navigational property EmployeeDepartment first to access the department data.
            //    //Or you can get the department name in "second" query based on "DepartmentId" of the "Emp01", Like in this case.
            //    var EmpDepartment = context.Departments.Where(D => D.DeptId == Emp01.DepartmentId).FirstOrDefault();
            //    if(EmpDepartment is not null)
            //        Console.WriteLine($"EmpDepartment Name:{EmpDepartment.Name}");//EmpDepartment Name:Sales
            //    //So there are 2 queries to get the related data.
            //    //One to get the Employee Data
            //    //Second to get the department name based on the EmployeeDepartmentId.
            //}

            #endregion

            //By default Navigational Properties [Related Data] Not Loaded.
            //There Are 3 ways to load the Navigational Property [Related Data]
            //  1.Eager Loading [Load Data And Related Data In One Query to DB] - [Load Related Data Even You Don't Need It]
            //  2.Explicit Loading [Load Data And Related Data In Two Query to DB] - Write Code To Do this [Load Related Data Explicit When You need].
            //  3.Lazy Loading(Implicit Loading) [Load Data And Related Data In Two Query to DB] - Write some configuration to make it Default of EFCORE [Load Related Data When You need/Write].

            //01.Eager Loading =>
            //   - Eager Loading means retrieving the main entity and it's related data in a single query
            //     using Include() - ThenInclude() 
            //   - ThenInclude() -> For Multi-Level relationship.

            #region Example02 - Return Employee With Code 2 and it's Department Name. 

            //var Emp01WithDepartment = context.Employees.Include(E => E.EmployeeDepartment).FirstOrDefault(E => E.Code == 2);
            ////This previous line loaded the Employee Data and also Related data of it's department "Sales" with Id = 90.
            ////Based on the Navigational property "EmployeeDepartment" and "FK" "DepartmentId".
            ////This happen in one query using Eager Loading
            ////- The Query Which executed on DB - is inner join query because the Navigational Property "EmployeeDepartment" is of type "Department" not of type nullable "Department?" so it's Mandatory Relationship
            ////    So Any Employee Must Has Department, So EFCORE execute this query As InnerJoin.
            ////- The Query Which executed on DB - will be Left join query if the Navigational Property "EmployeeDepartment" is of type Nullable "Department?" so it's Optional Relationship
            ////    So Any Employee May Has Department, So EFCORE execute this query As LeftJoin Query
            ////    To return All Employees Regardless they are attached to department or not.

            ////So Eager Loading Approach Get The Main Data And Related Data In One Query Using "Join".
            //// - Use "LeftJoin" When Relationship/NavigationalProperty is "Optional"
            //// - Use "InnerJoin" When Relationship/NavigationalProperty is "Mandatory"

            //if (Emp01WithDepartment is not null)
            //{
            //    Console.WriteLine($"Name:{Emp01WithDepartment.EmpName}");// Name: Sama
            //    Console.WriteLine($"DepartmentID:{Emp01WithDepartment.DepartmentId}");// DepartmentID:90
            //    Console.WriteLine($"DepartmentName:{Emp01WithDepartment.EmployeeDepartment?.Name}");// DepartmentName:Sales [Related Data]
            //}

            #endregion

            #region Example03 - Get The Employee With Code 2 And Name Of Department Which he Manage

            //var Emp01WithManagedDepartment = context.Employees.Include(E => E.ManagedDepartment)
            //                                                  .FirstOrDefault(E => E.Code == 2);

            //if(Emp01WithManagedDepartment is not null)
            //{
            //    Console.WriteLine($"Name:{Emp01WithManagedDepartment.EmpName}");// Name:Sama
            //    Console.WriteLine($"DepartmentID:{Emp01WithManagedDepartment.DepartmentId}");// DepartmentID:90
            //    Console.WriteLine($"ManagedDepartmentId:{Emp01WithManagedDepartment.ManagedDepartment?.DeptId}");// ManagedDepartmentId:100 [Related Data]
            //    Console.WriteLine($"ManagedDepartmentName:{Emp01WithManagedDepartment.ManagedDepartment?.Name}");// ManagedDepartmentName:Media [Related Data]
            //}

            ////In this Case The Relationship is "Optional" - Employee May has ManagedDepartment
            ////  public Department? ManagedDepartment { get; set; }  
            ////So the query executed in DB use the "LeftJoin" to return All Employees Regardless They manage department or not because i work on table Employees as Main Entity to return data of it.

            #endregion

            #region Example04 - Get The Employee With Code 2 and It's Department Name and name of manager of this department.

            //var Emp01WithDepartment = context.Employees.Include(E => E.EmployeeDepartment)
            //                                           .ThenInclude(D => D.Manager)//Include From Department Table [Load Navigational Property "Manager"]
            //                                           .FirstOrDefault(E => E.Code == 2);


            //if (Emp01WithDepartment is not null)
            //{
            //    Console.WriteLine($"Name:{Emp01WithDepartment.EmpName}");// Name:Sama
            //    Console.WriteLine($"DepartmentID:{Emp01WithDepartment.DepartmentId}");// DepartmentID:90
            //    Console.WriteLine($"DepartmentName:{Emp01WithDepartment.EmployeeDepartment?.Name}");// DepartmentName:Sales [Related Data]
            //    Console.WriteLine($"DepartmentManagerId:{Emp01WithDepartment.EmployeeDepartment?.Manager?.DepartmentId}");// DepartmentManagerId:90 [Related Data]
            //    Console.WriteLine($"DepartmentManagerName:{Emp01WithDepartment.EmployeeDepartment?.Manager?.EmpName}");// DepartmentManagerName:Sameh [Related Data]
            //}

            ////Load Data Of EmployeeDepartment Navigational Property [Data of Departments]
            ////And Data of Manager Navigational Property [Data of managers]

            #endregion

            #region Example05 - Get The Employees In Department "Sales"

            //var employeesInDepartmentHR = context.Employees.Include(E => E.EmployeeDepartment)
            //                                               .Where(E => E.EmployeeDepartment.Name == "Sales")
            //                                               .Select(E => new { E.EmpName, DepartmentName = E.EmployeeDepartment.Name });//To Select only EmpName from Table Employees and DepartmentName from Department Table - Not select all columns when this line executed in SQL.

            ////  SELECT[e].[EmpName], [d].[DepartmentName]
            ////  FROM[Employees] AS[e]
            ////  INNER JOIN[Sales].[Departments] AS[d] ON[e].[DepartmentId] = [d].[DeptId]
            ////  WHERE[d].[DepartmentName] = 'Sales'

            //if (employeesInDepartmentHR is not null)
            //{
            //    foreach (var emp in employeesInDepartmentHR)
            //    {
            //        Console.WriteLine(emp);
            //    }
            //}

            #endregion

            //When To Use Eager Loading Approach For Loading The Related Data/Navigational Property ?
            //  - If You Know That You Will Always Need The Related Data.
            //  - If You Need To Reduce Requests To Database [Just Send One Request To Bring Main Data And Related Data].
            #endregion

            #region Part 05 Loading Related Data - Explicit Loading



            #endregion

        }
    }
}
