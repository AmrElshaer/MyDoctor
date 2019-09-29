using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Data.Migrations
{
    public partial class addbeatyandReltive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeatyandHealthy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Catagory = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeatyandHealthy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelativeofBeatyandhealthy",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ImageOrvideo = table.Column<string>(nullable: true),
                    subject = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    beatyandHealthyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelativeofBeatyandhealthy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelativeofBeatyandhealthy_BeatyandHealthy_beatyandHealthyId",
                        column: x => x.beatyandHealthyId,
                        principalTable: "BeatyandHealthy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelativeofBeatyandhealthy_beatyandHealthyId",
                table: "RelativeofBeatyandhealthy",
                column: "beatyandHealthyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelativeofBeatyandhealthy");

            migrationBuilder.DropTable(
                name: "BeatyandHealthy");
        }
    }
}
