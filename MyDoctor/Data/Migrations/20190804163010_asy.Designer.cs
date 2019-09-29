﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyDoctor.Data;

namespace MyDoctor.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190804163010_asy")]
    partial class asy
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MyDoctor.Models.BeatyandHealthy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Catagory");

                    b.Property<string>("Image");

                    b.HasKey("Id");

                    b.ToTable("BeatyandHealthy");

                    b.HasData(
                        new { Id = 1, Catagory = "الرياضة والرشاقة", Image = "https://static.webteb.net/images/content/slideshow_slideshow_1756_3807b64a6cc-17e2-4ea3-9785-abd3cdb05540.jpg" },
                        new { Id = 2, Catagory = "الريجيم وتخفيف الوزن", Image = "https://static.webteb.net/images/content/tbl_articles_article_21104_50697899ac1-0b6d-454d-b882-aa5c535a839d.jpg" },
                        new { Id = 3, Catagory = "التغذية السليمة", Image = "https://static.webteb.net/images/content/tbl_articles_article_21149_393ab96dbff-e233-4161-8335-2590a210376c.jpg" },
                        new { Id = 4, Catagory = "الحياة الزوجية", Image = "https://static.webteb.net/images/content/slideshow_slideshow_1855_938129447e6-33b4-4d1f-87e9-cb69c44f463f.jpg" },
                        new { Id = 5, Catagory = "العناية بالشعر", Image = "https://static.webteb.net/images/content/video_video_574_19396f7560c-3586-42b6-8a33-c4242b762843.jpg" },
                        new { Id = 6, Catagory = "العناية بالبشرة والجمال", Image = "https://static.webteb.net/images/content/tbl_articles_article_21115_350a54688d-6b44-49de-9132-c83db36c1e21.jpg" },
                        new { Id = 7, Catagory = "جودة الحياة", Image = "https://static.webteb.net/images/content/tbl_articles_article_21091_224c1f7c9d9-1017-49f6-91ab-8338bf8f8bf5.jpg" },
                        new { Id = 8, Catagory = "الجيل الذهبي", Image = "https://static.webteb.net/images/content/tbl_articles_article_17490_98.jpg" },
                        new { Id = 9, Catagory = "وصفات صحية", Image = "https://static.webteb.net/images/content/ramadanrecipe_recipe_544_785238ad407-4bce-40df-a2e5-402e3fae7092.jpg" },
                        new { Id = 10, Catagory = "القيم الغذائية للمأكولات", Image = "https://static.webteb.net/images/content/tbl_articles_article_21183_468062634f5-e43b-4772-bd68-daaf7436c10f.jpg" }
                    );
                });

            modelBuilder.Entity("MyDoctor.Models.Comments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Commment");

                    b.Property<string>("DiseaseName");

                    b.Property<string>("ImagePath");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("MyDoctor.Models.CutomPropertiy", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("ImagePath");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MyDoctor.Models.Disease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiseaseName")
                        .IsRequired();

                    b.Property<string>("Image");

                    b.Property<string>("Medicin");

                    b.Property<string>("Protection");

                    b.Property<string>("Reasons");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.ToTable("Disease");
                });

            modelBuilder.Entity("MyDoctor.Models.LikeandDislikeclass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisLike");

                    b.Property<string>("DiseaseName");

                    b.Property<int>("Like");

                    b.HasKey("Id");

                    b.ToTable("LikeandDislikeclass");

                    b.HasData(
                        new { Id = 1, DisLike = 0, DiseaseName = "Highpress", Like = 0 }
                    );
                });

            modelBuilder.Entity("MyDoctor.Models.RelativeDisease", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DiseaseAddress");

                    b.Property<string>("DiseaseName");

                    b.Property<string>("ImageOrvideo");

                    b.Property<string>("subject");

                    b.HasKey("Id");

                    b.ToTable("RelativeDisease");
                });

            modelBuilder.Entity("MyDoctor.Models.RelativeofBeatyandhealthy", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<int>("BeatyId");

                    b.Property<string>("ImageOrvideo");

                    b.Property<string>("subject");

                    b.HasKey("Id");

                    b.ToTable("RelativeofBeatyandhealthy");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MyDoctor.Models.CutomPropertiy")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MyDoctor.Models.CutomPropertiy")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyDoctor.Models.CutomPropertiy")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MyDoctor.Models.CutomPropertiy")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
