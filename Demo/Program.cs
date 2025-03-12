using Castle.Components.DictionaryAdapter.Xml;
using Demo.Data.DbContexts;
using Demo.Data.Models;
using Demo.Data_Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Net.Http.Headers;
using static Azure.Core.HttpHeader;

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
            //  - If You Know That You Will Always Need The Related Data like if you need to print this related data.
            //  - If You Need To Reduce Requests To Database [Just Send One Request To Bring Main Data And Related Data].

            #endregion

            #region Part 05 Loading Related Data - Explicit Loading [Manual Loading]
            //Load Related Data/Navigational Property Manually After Load The Main Data 
            //So There Are 2 requests To DB -> 
            // First -> To Return The Main Data.
            // Second -> To Return The Related Data.

            //Explicit Loading Means manually loading related data after the main entity has already been retrieved 
            //Uses the Entry() method combined with Reference() -> For Selecting The Navigational Property Represents The "One" relationship
            //Uses the Entry() method combined with Collection() -> For Selecting The Navigational Property Represents The "Many" relationship

            #region Example01- Get The Employee With Code "2" And it's Department Data.

            //var Emp01WithDepartment = context.Employees.FirstOrDefault(E => E.Code == 2);//First Request to DB to Return Employee object/data With Code == 2.

            //if(Emp01WithDepartment is not null)//I need to load the related data/Navigational Property to print "DepartmentName".
            //{
            //    Console.WriteLine($"Name:{Emp01WithDepartment.EmpName}");// Name: Sama
            //    Console.WriteLine($"DepartmentID:{Emp01WithDepartment.DepartmentId}");// DepartmentID:90
            //    context.Entry(Emp01WithDepartment).Reference(E => E.EmployeeDepartment).Load();//Second Request to DB To Load The Navigational Property "EmployeeDepartment" Explicitly
            //    Console.WriteLine($"DepartmentName:{Emp01WithDepartment.EmployeeDepartment?.Name}");// DepartmentName:Sales [Related Data]
            //}

            //1- Get The Main Data in First Request
            //2- Get The Related Data in Second Request

            #endregion

            #region Example02 - Get Department With Id = 90 And it's Employees

            //var Department = context.Departments.FirstOrDefault(D => D.DeptId == 90);//By Default This Query Not load the relatedData/NavigationalProperty of this object which is the EmployeeDepartment which he is related to.

            //if (Department is not null)
            //{
            //    Console.WriteLine($"DepartmentName:{Department.Name}");
            //    Console.WriteLine();
            //    context.Entry(Department).Collection(D => D.Employees).Load();//Load The Navigational Property "Many" To Print The Employees objects/data in this department Explicitly.
            //    foreach (var emp in Department.Employees)
            //    {
            //        Console.WriteLine($"Code:{emp.Code} - Name:{emp.EmpName}");
            //    }
            //}

            #endregion

            #region Example03 - Get the Employees in Department 90 and Age more than 25.

            //var Department = context.Departments.FirstOrDefault(D => D.DeptId == 90);

            //if (Department is not null)
            //{
            //    Console.WriteLine($"DepartmentName:{Department.Name}");
            //    Console.WriteLine();
            //    context.Entry(Department).Collection(D => D.Employees).Query().Where(E => E.Age>25).Load();//Make Query on the returned collection of employees to filter them Age>25.
            //    foreach (var emp in Department.Employees)
            //    {
            //        Console.WriteLine($"Code:{emp.Code} - Name:{emp.EmpName}");
            //    }
            //}

            #endregion

            //When To Use Explicit Loading Approach For Loading The Related Data/Navigational Property ?
            //  - If you need to control when to load the related data to work on it.
            //  - If you need to optimize performance by loading only what you need - not load data that you don't need it
            //      like if use Eager Loading and don't work on the related data after load it
            //  - With Explicit Loading - By default the related data of the main object not loaded unless you load it manually.            

            #endregion

            #region Part 06 Loading Related Data - Lazy Loading
            ///Lazy Loading means that related data of the main object is not loaded from the Database
            /// until it's accessed for the first time/When use it 
            /// So EF Core delays the loading of navigational properties until you explicitly use them
            /// But EF Core Doesn't Enable Lazy Loading By Default [By Default is The Navigational Property Not Loaded Until You Explicit Load it]
            ///So you need to Do extra configuration Manually To Enable Lazy Loading Environment [To Make The Default Of EF Core is load the navigational property when use it direct]. 
            ///
            ///Configure Lazy Loading Feature : 
            /// 1.Install the package (Microsoft.EntityFrameWorkCore.Proxies) in your project.
            /// 2.Configure Lazy Loading in your DbContext in The OnConfiguring() method -> "optionsBuilder.UseLazyLoadingProxies();"
            /// 3.Make Access Modifier for All Model Classes "public"
            ///     Or put this attribute on the namespace of the model class/on the file of your model class to make this class visible for the assembly "DynamicProxyGenAssembly2" -> [assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
            /// 4.Make All Navigational Properties "Virtual"
            ///
            ///Why make classes "public" and Navigational properties "Virtual"?
            /// - Because When Enable the Lazy Loading Environment
            /// - EF Core Will make Proxy Class For each and every class model in your project
            ///    And override the navigational properties which is in the model class
            ///    To override behavior of get() method of this navigational property 
            ///    To make it returned the data instead of return "null"  when call the property - [Load the data].
            /// - So Must Make The Navigational properties "Virtual" To override it in the proxy class which inherit from model class
            /// - Make Model Classes As "Public" because the proxy classes inherit from it in run time
            /// - And those proxy classes Are in/CameFrom External package not from current project. 

            #region Example01 - Get The Employee With Code "2" And it's Department Data.

            //var Emp01WithDepartment = context.Employees.FirstOrDefault(E => E.Code == 2);

            //if (Emp01WithDepartment is not null)
            //{
            //    Console.WriteLine($"Name:{Emp01WithDepartment.EmpName}");//Name: Sama
            //    Console.WriteLine($"DepartmentID:{Emp01WithDepartment.DepartmentId}");//DepartmentID:90
            //    Console.WriteLine($"DepartmentName:{Emp01WithDepartment.EmployeeDepartment?.Name}");//DepartmentName:Sales [Related Data Loaded Dynamically/ByDefault when use/call the Navigational property After Enable Lazy Loading Environment]
            //}

            #endregion

            #region Example02 - Get Department With Id = 90 And it's Employees

            //var Department = context.Departments.FirstOrDefault(D => D.DeptId == 90);//First Request To DB

            //if (Department is not null)
            //{
            //    Console.WriteLine($"DepartmentName:{Department.Name}");//Main Data
            //    Console.WriteLine();
            //    foreach (var emp in Department.Employees)
            //    {
            //        Console.WriteLine($"Code:{emp.Code} - Name:{emp.EmpName}");//Related Data [Second Request To DB]
            //    }

            //    //DepartmentName: Sales

            //    //Code:2 - Name:Sama
            //    //Code:4 - Name:Soha
            //    //Code:6 - Name:Sameh
            //    //Code:7 - Name:Pola

            //}

            #endregion

            //When To Use Lazy Loading Approach For Loading The Related Data/Navigational Property ?
            //  - If you need to control when to load the related data to work on it.
            //  - You Want To reduce the size of initial query.
            //  - Return Related Data without Write Explicit Code.

            #endregion

            #region Part 07 Joins Category - Join()
            //Combining Data from multiple Collections Or Tables.

            //Inner Join Join():
            // Returns matching records from both tables based on a condition

            #region Example01 - Get The Departments That Has Employees and Display EmpId-EmpName-DeptId-DeptName [work RelationShip]

            //Department has many Employees [work] in it & One Employee Work in One Department.
            // So we take Pk of Employees Table [One] As FK in Departments Table. 
            //Hold First the Table That has the "PK" of the relationship "Departments"

            #region 01 Fluent Syntax

            //var result = context.Departments.Join(context.Employees
            //                                          , D => D.DeptId/*Outer Selector -> PK*/
            //                                          , E => E.DepartmentId/*Inner Selector ->FK*/
            //                                          , (D, E) => new /*Result Selector*/
            //                                          {
            //                                              EmployeeCode = E.Code,
            //                                              EmployeeName = E.EmpName,
            //                                              DepartmentId = D.DeptId,
            //                                              DepartmentName = D.Name
            //                                          });
            ////SELECT[e].[Code] AS[EmployeeCode], [e].[EmpName] AS[EmployeeName], [d].[DeptId] AS[DepartmentId], [d].[DepartmentName]
            ////FROM[Sales].[Departments] AS[d]
            ////INNER JOIN[Employees] AS[e] ON[d].[DeptId] = [e].[DepartmentId]
            //foreach (var item in result)
            //{
            //    //Console.WriteLine($"EmployeeCode:{item.EmployeeCode}, EmployeeName:{item.EmployeeName}, DepartmentId:{item.DepartmentId}, DepartmentName:{item.DepartmentName}");
            //    Console.WriteLine(item);
            //}
            ////{ EmployeeCode = 2, EmployeeName = Sama, DepartmentId = 90, DepartmentName = Sales }
            ////{ EmployeeCode = 3, EmployeeName = Nadia, DepartmentId = 110, DepartmentName = Markting }
            ////{ EmployeeCode = 4, EmployeeName = Soha, DepartmentId = 90, DepartmentName = Sales }
            ////{ EmployeeCode = 5, EmployeeName = Mazen, DepartmentId = 110, DepartmentName = Markting }
            ////{ EmployeeCode = 6, EmployeeName = Sameh, DepartmentId = 90, DepartmentName = Sales }
            ////{ EmployeeCode = 7, EmployeeName = Pola, DepartmentId = 90, DepartmentName = Sales }

            #endregion

            #region 02 Query Syntax

            //var result = from D in context.Departments
            //             join E in context.Employees
            //             on D.DeptId equals E.DepartmentId
            //             select new
            //             {
            //                 EmployeeCode = E.Code,
            //                 EmployeeName = E.EmpName,
            //                 DepartmentId = D.DeptId,
            //                 DepartmentName = D.Name
            //             };

            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);
            //}

            #endregion

            #endregion

            #region Example02 - Get The Employees that are managers and select Department Id - Department Name  - Manager Id - Manager Name.
            //Department Has One Manager And Employee Manage one department.
            //Department Must Has Manager - Employee May Manage Department
            //Take "PK" of May "Employee" "Code" As "FK" in "Department".

            #region 01 Fluent Syntax

            //var result = context.Employees.Join(context.Departments
            //                                       , E => E.Code
            //                                       , D => D.DeptManagerId
            //                                       , (E, D) => new
            //                                       {
            //                                           ManagedDepartmentId = D.DeptId,
            //                                           ManagedDepartmentName = D.Name,
            //                                           ManagerId = E.Code,
            //                                           ManagerName = E.EmpName
            //                                       });

            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);
            //}
            ////{ ManagedDepartmentId = 90, ManagedDepartmentName = Sales, ManagerId = 6, ManagerName = Sameh }
            ////{ ManagedDepartmentId = 100, ManagedDepartmentName = Media, ManagerId = 2, ManagerName = Sama }

            #endregion

            #region 02 Query Syntax

            //var result = from E in context.Employees
            //             join D in context.Departments
            //             on E.Code equals D.DeptManagerId
            //             select new
            //             {
            //                 ManagedDepartmentId = D.DeptId,
            //                 ManagedDepartmentName = D.Name,
            //                 ManagerId = E.Code,
            //                 ManagerName = E.EmpName
            //             };

            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);
            //}
            ////{ ManagedDepartmentId = 90, ManagedDepartmentName = Sales, ManagerId = 6, ManagerName = Sameh }
            ////{ ManagedDepartmentId = 100, ManagedDepartmentName = Media, ManagerId = 2, ManagerName = Sama }

            #endregion

            #endregion

            #endregion

        }
    }
}
