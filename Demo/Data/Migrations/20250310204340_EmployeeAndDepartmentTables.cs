using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeAndDepartmentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Sales");

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "Sales",
                columns: table => new
                {
                    DeptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "10, 10"),
                    DepartmentName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValue: "HR"),
                    DateOfCreation = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GetDate()"),
                    DeptManagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DeptId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Sales",
                        principalTable: "Departments",
                        principalColumn: "DeptId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DeptManagerId",
                schema: "Sales",
                table: "Departments",
                column: "DeptManagerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_DeptManagerId",
                schema: "Sales",
                table: "Departments",
                column: "DeptManagerId",
                principalTable: "Employees",
                principalColumn: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_DeptManagerId",
                schema: "Sales",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "Sales");
        }
    }
}
