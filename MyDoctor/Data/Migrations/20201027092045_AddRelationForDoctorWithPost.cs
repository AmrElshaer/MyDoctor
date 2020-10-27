using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Data.Migrations
{
    public partial class AddRelationForDoctorWithPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Doctorid",
                table: "Posts",
                newName: "DoctorId");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_DoctorId",
                table: "Posts",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Doctor_DoctorId",
                table: "Posts",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Doctor_DoctorId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_DoctorId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Posts",
                newName: "Doctorid");

            migrationBuilder.AlterColumn<string>(
                name: "Doctorid",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
