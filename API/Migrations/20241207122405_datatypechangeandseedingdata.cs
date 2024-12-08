using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class datatypechangeandseedingdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "EmployeePhone",
                table: "Employees",
                type: "bigint",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Description", "Designation", "EmployeeAge", "EmployeePhone", "Name" },
                values: new object[,]
                {
                    { 1, "developer with 3 years of experience", "Developer", 24, 7894561230L, "Prakash" },
                    { 2, "tester with 1 year of experience", "Tester", 22, 123654789L, "Ganesh" },
                    { 3, "DA with 1 year of experience", "DA", 25, 123654259L, "Om" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeePhone",
                table: "Employees",
                type: "int",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 10);
        }
    }
}
