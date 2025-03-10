using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Demo.Data.Migrations
{
    /// <inheritdoc />
    public partial class DepartmentDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Sales",
                table: "Departments",
                columns: new[] { "DeptId", "DateOfCreation", "DeptManagerId", "DepartmentName" },
                values: new object[,]
                {
                    { 70, new DateOnly(2024, 12, 13), null, "Design" },
                    { 80, new DateOnly(2024, 1, 13), null, "Software" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Sales",
                table: "Departments",
                keyColumn: "DeptId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                schema: "Sales",
                table: "Departments",
                keyColumn: "DeptId",
                keyValue: 80);
        }
    }
}
