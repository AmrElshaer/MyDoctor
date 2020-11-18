using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Migrations
{
    public partial class updatediseasemedicin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseMedicin_Disease_DiseaseId",
                table: "DiseaseMedicin");

            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseMedicin_Medicin_MedicinId",
                table: "DiseaseMedicin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiseaseMedicin",
                table: "DiseaseMedicin");

            migrationBuilder.RenameTable(
                name: "DiseaseMedicin",
                newName: "DiseaseMedicins");

            migrationBuilder.RenameIndex(
                name: "IX_DiseaseMedicin_MedicinId",
                table: "DiseaseMedicins",
                newName: "IX_DiseaseMedicins_MedicinId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiseaseMedicins",
                table: "DiseaseMedicins",
                columns: new[] { "DiseaseId", "MedicinId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseMedicins_Disease_DiseaseId",
                table: "DiseaseMedicins",
                column: "DiseaseId",
                principalTable: "Disease",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseMedicins_Medicin_MedicinId",
                table: "DiseaseMedicins",
                column: "MedicinId",
                principalTable: "Medicin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseMedicins_Disease_DiseaseId",
                table: "DiseaseMedicins");

            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseMedicins_Medicin_MedicinId",
                table: "DiseaseMedicins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiseaseMedicins",
                table: "DiseaseMedicins");

            migrationBuilder.RenameTable(
                name: "DiseaseMedicins",
                newName: "DiseaseMedicin");

            migrationBuilder.RenameIndex(
                name: "IX_DiseaseMedicins_MedicinId",
                table: "DiseaseMedicin",
                newName: "IX_DiseaseMedicin_MedicinId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiseaseMedicin",
                table: "DiseaseMedicin",
                columns: new[] { "DiseaseId", "MedicinId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseMedicin_Disease_DiseaseId",
                table: "DiseaseMedicin",
                column: "DiseaseId",
                principalTable: "Disease",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseMedicin_Medicin_MedicinId",
                table: "DiseaseMedicin",
                column: "MedicinId",
                principalTable: "Medicin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
