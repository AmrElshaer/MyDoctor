using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Migrations
{
    public partial class UpdateDoctorDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Specials",
                table: "Doctor",
                newName: "ImagePath");

            migrationBuilder.AlterColumn<string>(
                name: "Kind",
                table: "Doctor",
                type: "nvarchar(24)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Doctor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_CategoryId",
                table: "Doctor",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_BeatyandHealthy_CategoryId",
                table: "Doctor",
                column: "CategoryId",
                principalTable: "BeatyandHealthy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_BeatyandHealthy_CategoryId",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_CategoryId",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Doctor");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Doctor",
                newName: "Specials");

            migrationBuilder.AlterColumn<string>(
                name: "Kind",
                table: "Doctor",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(24)");
        }
    }
}
