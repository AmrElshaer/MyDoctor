using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Data.Migrations
{
    public partial class dfs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LikeandDislikeclass",
                columns: new[] { "Id", "DisLike", "DiseaseName", "Like" },
                values: new object[] { 1, 0, "Highpress", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LikeandDislikeclass",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
