using Microsoft.EntityFrameworkCore.Migrations;

namespace MYDoctor.Infrastructure.Migrations
{
    public partial class seedUseProfileData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "0a7482f7-c62c-4e43-b34c-b6e07413f9a2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "5d0226cb-5852-4c24-80f3-72641724bd4d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "e6f09ba8-3456-4c48-8f8f-c4c26d932304");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18b8b6f9-891d-497a-8fc1-94f35a425147", "AQAAAAEAACcQAAAAEE10PZmxgpPcnG2eXZ1QJmOEAp3ugEVAvuTlVkOSY/xbkeeqAq4i0MeNxVGdHVn8bw==", "a590e324-802c-4dc2-ba8d-ec9972413d3d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "2c99b4d0-431a-4205-a241-004245536333");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "60ff0a91-817b-4dba-b302-91c12c1b7f2f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "e15e487b-8d37-4c32-9360-0e5624dc0ec8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "375b6a30-da44-4b02-8378-70eac208973c", "AQAAAAEAACcQAAAAEGhp3f6Uwcd+W7SO2cPT2534YuiExEDCuLjRZjqbJ2pzVT4GDJnbd8GEVfcytszSAw==", "d5282a19-fe28-42b6-8a58-ed8b2e9594f3" });
        }
    }
}
