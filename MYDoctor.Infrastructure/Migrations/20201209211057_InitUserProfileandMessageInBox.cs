using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MYDoctor.Infrastructure.Migrations
{
    public partial class InitUserProfileandMessageInBox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InboxMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UserProfileId = table.Column<int>(nullable: true),
                    ToUserProfileId = table.Column<int>(nullable: true),
                    IsSee = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InboxMessages_UserProfiles_ToUserProfileId",
                        column: x => x.ToUserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InboxMessages_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "563c65b4-2086-4789-a3ba-4a3afc352bc0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "410e4986-6e57-42a4-ac57-a49ee2e7bf91");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "45693e9e-36d9-41c2-8fb2-bfb16559aaca");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fbc2009e-89be-4166-b480-9c018b729849", "AQAAAAEAACcQAAAAEBu4akLm281iw3OnN5N7daLLvFR3x+ZrVM9+UmlrS5y5LKt92ftRhVrg6is1OwOByg==", "a8fa7e8f-dd6a-4b68-9eb0-597ef962b3ca" });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "Email", "ImagePath", "Name" },
                values: new object[] { 1, "Admin@Admin.com", null, "Admin@Admin.com" });

            migrationBuilder.CreateIndex(
                name: "IX_InboxMessages_ToUserProfileId",
                table: "InboxMessages",
                column: "ToUserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxMessages_UserProfileId",
                table: "InboxMessages",
                column: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InboxMessages");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "fbd3da17-45bd-44aa-8542-cdc6d74ccbe1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "b8662be8-e7bc-4616-a842-1198055486e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "1aa5bcae-4ef2-45ee-911b-fed7bd80f9b5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f5aae16-6473-405c-86de-412406152b05", "AQAAAAEAACcQAAAAEK3EXpyIq4dyHCtOmIUzkx332PdXiL6cVHIxskomi/gQ55j10oY6k/g/qTJ5q8T2Qg==", "baec1316-a2f1-48d5-a02e-c248404ece61" });
        }
    }
}
