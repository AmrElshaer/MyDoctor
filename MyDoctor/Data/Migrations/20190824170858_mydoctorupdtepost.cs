using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Data.Migrations
{
    public partial class mydoctorupdtepost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Disease",
                table: "Posts",
                newName: "specific");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "specific",
                table: "Posts",
                newName: "Disease");
        }
    }
}
