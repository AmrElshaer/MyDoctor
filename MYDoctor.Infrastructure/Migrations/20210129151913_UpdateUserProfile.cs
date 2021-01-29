using Microsoft.EntityFrameworkCore.Migrations;

namespace MYDoctor.Infrastructure.Migrations
{
    public partial class UpdateUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3232defb-d35d-4298-b48e-e71fc334a29b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "d90a9dbb-0665-4f87-9178-75c67b258c0e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "e3504c2f-8898-4f65-b159-8636686077ee");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5b818961-6f0c-46d1-8dc8-0b470b90ed52", "AQAAAAEAACcQAAAAEOKDi1LVaryul0Wa3fa2BPdLX+JkS33ePx1yKsFZFGAhJZRPmdQSbpte2Xe6YNJirg==", "9d2d385a-4537-47a0-9f75-839e2c01e7b4" });
        }
    }
}
