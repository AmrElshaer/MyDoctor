using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Migrations
{
    public partial class DiseaseOperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Medicin",
                table: "Disease");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Medicin",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Medicin",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "indications",
                table: "Medicin",
                newName: "Indications");

            migrationBuilder.RenameColumn(
                name: "affects",
                table: "Medicin",
                newName: "Affects");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Medicin",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "diseasespecific",
                table: "Medicin",
                newName: "Image");

            migrationBuilder.AddColumn<int>(
                name: "BeatyandHealthyId",
                table: "Medicin",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Medicin",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Medicin",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Medicin",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BeatyandHealthyId",
                table: "Disease",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Disease",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Disease",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DiseaseMedicin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    DiseaseId = table.Column<int>(nullable: false),
                    MedicinId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseMedicin", x => new { x.DiseaseId, x.MedicinId });
                    table.ForeignKey(
                        name: "FK_DiseaseMedicin_Disease_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Disease",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiseaseMedicin_Medicin_MedicinId",
                        column: x => x.MedicinId,
                        principalTable: "Medicin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicin_BeatyandHealthyId",
                table: "Medicin",
                column: "BeatyandHealthyId");

            migrationBuilder.CreateIndex(
                name: "IX_Disease_BeatyandHealthyId",
                table: "Disease",
                column: "BeatyandHealthyId");

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseMedicin_MedicinId",
                table: "DiseaseMedicin",
                column: "MedicinId");

            migrationBuilder.AddForeignKey(
                name: "FK_Disease_BeatyandHealthy_BeatyandHealthyId",
                table: "Disease",
                column: "BeatyandHealthyId",
                principalTable: "BeatyandHealthy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicin_BeatyandHealthy_BeatyandHealthyId",
                table: "Medicin",
                column: "BeatyandHealthyId",
                principalTable: "BeatyandHealthy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disease_BeatyandHealthy_BeatyandHealthyId",
                table: "Disease");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicin_BeatyandHealthy_BeatyandHealthyId",
                table: "Medicin");

            migrationBuilder.DropTable(
                name: "DiseaseMedicin");

            migrationBuilder.DropIndex(
                name: "IX_Medicin_BeatyandHealthyId",
                table: "Medicin");

            migrationBuilder.DropIndex(
                name: "IX_Disease_BeatyandHealthyId",
                table: "Disease");

            migrationBuilder.DropColumn(
                name: "BeatyandHealthyId",
                table: "Medicin");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Medicin");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Medicin");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Medicin");

            migrationBuilder.DropColumn(
                name: "BeatyandHealthyId",
                table: "Disease");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Disease");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Disease");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Medicin",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Medicin",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Indications",
                table: "Medicin",
                newName: "indications");

            migrationBuilder.RenameColumn(
                name: "Affects",
                table: "Medicin",
                newName: "affects");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Medicin",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Medicin",
                newName: "diseasespecific");

            migrationBuilder.AddColumn<string>(
                name: "Medicin",
                table: "Disease",
                nullable: true);
        }
    }
}
