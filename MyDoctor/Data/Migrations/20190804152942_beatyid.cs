using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Data.Migrations
{
    public partial class beatyid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BeatyId",
                table: "RelativeofBeatyandhealthy",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeatyId",
                table: "RelativeofBeatyandhealthy");
        }
    }
}
