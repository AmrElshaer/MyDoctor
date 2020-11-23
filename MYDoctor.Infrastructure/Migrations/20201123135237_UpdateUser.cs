using Microsoft.EntityFrameworkCore.Migrations;

namespace MYDoctor.Infrastructure.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "d0b70bd4-6a8a-4fa8-9c0f-f6e4ab57e4ea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "ad069b79-09e4-4026-bb4e-798af9377e6c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "b5160393-2c75-437c-bf53-6b57b0cc7983");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60d01e0b-192b-44ef-8a7a-c32914a122c2", "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAENY8FfazXP8GIpPm1JPCMEhwnCnVpsHyj9MNhwxJ95vYWWJD+TCM10hGc24Xr0B3/w==", "04e269f5-5d52-4ce9-9f6c-3030736d9481" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc402172-8403-475e-a196-b66a50218af3", "Admin@Admin.com", "Admin@Admin.com", "AQAAAAEAACcQAAAAEFFFZuZflj+0DUp0gUb3q20RNJYJ27WoAq/1dZ7XssY6+AspDZsv9DVl2EXe5tD5sw==", null });
        }
    }
}
