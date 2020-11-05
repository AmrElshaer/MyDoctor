using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Migrations
{
    public partial class AddRelationwithMedicinandDisease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicin_BeatyandHealthy_BeatyandHealthyId",
                table: "Medicin");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Medicin");

            migrationBuilder.AlterColumn<int>(
                name: "BeatyandHealthyId",
                table: "Medicin",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicin_BeatyandHealthy_BeatyandHealthyId",
                table: "Medicin",
                column: "BeatyandHealthyId",
                principalTable: "BeatyandHealthy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicin_BeatyandHealthy_BeatyandHealthyId",
                table: "Medicin");

            migrationBuilder.AlterColumn<int>(
                name: "BeatyandHealthyId",
                table: "Medicin",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Medicin",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicin_BeatyandHealthy_BeatyandHealthyId",
                table: "Medicin",
                column: "BeatyandHealthyId",
                principalTable: "BeatyandHealthy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
