using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Data.Migrations
{
    public partial class addkind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kind",
                table: "Doctor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kind",
                table: "Doctor");
        }
    }
}
