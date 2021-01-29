using Microsoft.EntityFrameworkCore.Migrations;

namespace MYDoctor.Infrastructure.Migrations
{
    public partial class adduserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "1", "1" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "1995054b-a7f0-4e8c-9e88-b2a7573cdb61");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "12208e16-42a1-4392-bdf1-440067aa536e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "e3950792-09a2-4cf3-a8b1-a5eac158d7af");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "1", "1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d8507d7-a7b7-4c14-86ae-53224f99163b", "AQAAAAEAACcQAAAAEP7EEeHVymnQXdwHX0dHtFhw91AwarQyKMAqjH85rAWy1DCye68HXCDxWMCZtQC5gA==", "be6ba2bb-8a50-4cf1-b2d1-84f82e55a573" });
        }
    }
}
