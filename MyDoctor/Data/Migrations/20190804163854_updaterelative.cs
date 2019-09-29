using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Data.Migrations
{
    public partial class updaterelative : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "updateRelativeBeatyandHealthy",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ImageOrvideo = table.Column<string>(nullable: true),
                    subject = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    BeatyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_updateRelativeBeatyandHealthy", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "updateRelativeBeatyandHealthy");
        }
    }
}
