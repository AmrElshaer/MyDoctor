using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Data.Migrations
{
    public partial class asdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelativeofBeatyandhealthy_BeatyandHealthy_beatyandHealthyId",
                table: "RelativeofBeatyandhealthy");

            migrationBuilder.DropIndex(
                name: "IX_RelativeofBeatyandhealthy_beatyandHealthyId",
                table: "RelativeofBeatyandhealthy");

            migrationBuilder.DropColumn(
                name: "beatyandHealthyId",
                table: "RelativeofBeatyandhealthy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "beatyandHealthyId",
                table: "RelativeofBeatyandhealthy",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelativeofBeatyandhealthy_beatyandHealthyId",
                table: "RelativeofBeatyandhealthy",
                column: "beatyandHealthyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelativeofBeatyandhealthy_BeatyandHealthy_beatyandHealthyId",
                table: "RelativeofBeatyandhealthy",
                column: "beatyandHealthyId",
                principalTable: "BeatyandHealthy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
