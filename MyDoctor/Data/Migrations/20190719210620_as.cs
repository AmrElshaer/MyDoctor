using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Data.Migrations
{
    public partial class @as : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RelativeDisease",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DiseaseName = table.Column<string>(nullable: true),
                    ImageOrvideo = table.Column<string>(nullable: true),
                    subject = table.Column<string>(nullable: true),
                    DiseaseAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelativeDisease", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelativeDisease");
        }
    }
}
