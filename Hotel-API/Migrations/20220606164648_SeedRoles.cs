using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel_API.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "administrator", "00000000-0000-0000-0000-000000000000", "Administrator", "ADMINISTRATOR" },
                    { "guest", "00000000-0000-0000-0000-000000000000", "Guest", "GUEST" },
                    { "user", "00000000-0000-0000-0000-000000000000", "User", "USER" },
                    { "superuser", "00000000-0000-0000-0000-000000000000", "Superuser", "SUPERUSER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "administrator");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "guest");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "superuser");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user");
        }
    }
}
