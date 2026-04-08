using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonExample.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38e9aa07-71c1-4f73-8c5b-089c7085f260", "0c52073b-ea87-4e62-9dc8-47da97a6090e", "Guest", "GUEST" },
                    { "94f47fb3-e3cc-499e-8ae1-babe4be13766", "6adbe3a5-99f3-4f1e-92f0-9a1e534cf89e", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38e9aa07-71c1-4f73-8c5b-089c7085f260");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94f47fb3-e3cc-499e-8ae1-babe4be13766");
        }
    }
}
