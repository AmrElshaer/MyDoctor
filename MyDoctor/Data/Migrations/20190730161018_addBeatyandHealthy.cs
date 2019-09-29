using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDoctor.Data.Migrations
{
    public partial class addBeatyandHealthy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BeatyandHealthy",
                columns: new[] { "Id", "Catagory", "Image" },
                values: new object[,]
                {
                    { 1, "الرياضة والرشاقة", "https://static.webteb.net/images/content/slideshow_slideshow_1756_3807b64a6cc-17e2-4ea3-9785-abd3cdb05540.jpg" },
                    { 2, "الريجيم وتخفيف الوزن", "https://static.webteb.net/images/content/tbl_articles_article_21104_50697899ac1-0b6d-454d-b882-aa5c535a839d.jpg" },
                    { 3, "التغذية السليمة", "https://static.webteb.net/images/content/tbl_articles_article_21149_393ab96dbff-e233-4161-8335-2590a210376c.jpg" },
                    { 4, "الحياة الزوجية", "https://static.webteb.net/images/content/slideshow_slideshow_1855_938129447e6-33b4-4d1f-87e9-cb69c44f463f.jpg" },
                    { 5, "العناية بالشعر", "https://static.webteb.net/images/content/video_video_574_19396f7560c-3586-42b6-8a33-c4242b762843.jpg" },
                    { 6, "العناية بالبشرة والجمال", "https://static.webteb.net/images/content/tbl_articles_article_21115_350a54688d-6b44-49de-9132-c83db36c1e21.jpg" },
                    { 7, "جودة الحياة", "https://static.webteb.net/images/content/tbl_articles_article_21091_224c1f7c9d9-1017-49f6-91ab-8338bf8f8bf5.jpg" },
                    { 8, "الجيل الذهبي", "https://static.webteb.net/images/content/tbl_articles_article_17490_98.jpg" },
                    { 9, "وصفات صحية", "https://static.webteb.net/images/content/ramadanrecipe_recipe_544_785238ad407-4bce-40df-a2e5-402e3fae7092.jpg" },
                    { 10, "القيم الغذائية للمأكولات", "https://static.webteb.net/images/content/tbl_articles_article_21183_468062634f5-e43b-4772-bd68-daaf7436c10f.jpg" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BeatyandHealthy",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BeatyandHealthy",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BeatyandHealthy",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BeatyandHealthy",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BeatyandHealthy",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BeatyandHealthy",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BeatyandHealthy",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "BeatyandHealthy",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "BeatyandHealthy",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "BeatyandHealthy",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
