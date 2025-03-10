using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeTheDeptManagerIdForeignKeyAllowsNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Departments_DeptManagerId",
                schema: "Sales",
                table: "Departments");

            migrationBuilder.AlterColumn<int>(
                name: "DeptManagerId",
                schema: "Sales",
                table: "Departments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DeptManagerId",
                schema: "Sales",
                table: "Departments",
                column: "DeptManagerId",
                unique: true,
                filter: "[DeptManagerId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Departments_DeptManagerId",
                schema: "Sales",
                table: "Departments");

            migrationBuilder.AlterColumn<int>(
                name: "DeptManagerId",
                schema: "Sales",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DeptManagerId",
                schema: "Sales",
                table: "Departments",
                column: "DeptManagerId",
                unique: true);
        }
    }
}
