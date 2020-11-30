using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MYDoctor.Infrastructure.Migrations
{
    public partial class InitUserTableTrackTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableTrackUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    LastUpdateSee = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableTrackUsers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "fbd3da17-45bd-44aa-8542-cdc6d74ccbe1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "b8662be8-e7bc-4616-a842-1198055486e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "1aa5bcae-4ef2-45ee-911b-fed7bd80f9b5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f5aae16-6473-405c-86de-412406152b05", "AQAAAAEAACcQAAAAEK3EXpyIq4dyHCtOmIUzkx332PdXiL6cVHIxskomi/gQ55j10oY6k/g/qTJ5q8T2Qg==", "baec1316-a2f1-48d5-a02e-c248404ece61" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableTrackUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "5d9c184d-10ac-4202-8e57-229e5e534679");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "4a16a557-5d02-41b8-a140-38324280fcbe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "6eb26f52-779d-4f39-9bab-3e39be18b2d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63db1712-6a75-4987-a333-4bba29a5cab1", "AQAAAAEAACcQAAAAECMhLKatlK2bGaSClhHK1GdPBpZSdafSTWh/m/o+RwUuS//o+ClJtweboXddmSySOQ==", "7eaad654-4b8c-442d-a81a-64a198f16c3c" });
        }
    }
}
