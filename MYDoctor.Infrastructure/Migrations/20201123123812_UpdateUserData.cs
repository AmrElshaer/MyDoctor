using Microsoft.EntityFrameworkCore.Migrations;

namespace MYDoctor.Infrastructure.Migrations
{
    public partial class UpdateUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "0b5d56b3-3e14-41a0-9fa8-f2437e13fbe2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "ed74f040-6aa9-47a9-b5ed-473a3f78b3ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "0e604824-5f37-4a7c-a447-bf925d603866");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "LockoutEnabled", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "bc402172-8403-475e-a196-b66a50218af3", false, true, "Admin@Admin.com", "Admin@Admin.com", "AQAAAAEAACcQAAAAEFFFZuZflj+0DUp0gUb3q20RNJYJ27WoAq/1dZ7XssY6+AspDZsv9DVl2EXe5tD5sw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "65d48e0d-6012-4b8a-a2aa-c9adea01d00b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "c39e351f-a679-4149-8b15-66c6e5cd7a30");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "a1d2e550-12ad-4149-a8de-ced6b3edddec");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "LockoutEnabled", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "1f016841-ed26-46f4-b227-8df6a34d3192", true, false, null, null, "AQAAAAEAACcQAAAAEJWGsjcOukXm4fn6LajwQWvNJyTqhaXSPx7blRYCCoFKagoZj5jPQrbjL6hcu473Tw==" });
        }
    }
}
