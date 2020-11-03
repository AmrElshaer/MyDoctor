using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Migrations
{
    public partial class AddCreateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "RelativeofBeatyandhealthy",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModiteDate",
                table: "RelativeofBeatyandhealthy",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "RelativeofBeatyandhealthy");

            migrationBuilder.DropColumn(
                name: "ModiteDate",
                table: "RelativeofBeatyandhealthy");
        }
    }
}
