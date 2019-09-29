using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HealthandMedicinComment");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "HealthandMedicinComment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "HealthandMedicinComment");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "HealthandMedicinComment",
                nullable: false,
                defaultValue: 0);
        }
    }
}
