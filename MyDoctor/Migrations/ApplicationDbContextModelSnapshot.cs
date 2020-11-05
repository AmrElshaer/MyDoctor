﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyDoctor.Data;

namespace MyDoctor.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
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
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

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

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MyDoctor.Models.BeatyandHealthy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("Image");

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("Id");

                    b.ToTable("BeatyandHealthy");
                });

            modelBuilder.Entity("MyDoctor.Models.Commentfordoctorpost", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("containt");

                    b.Property<string>("postid");

                    b.Property<string>("userid");

                    b.HasKey("id");

                    b.ToTable("Commentfordoctorpost");
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

                    b.Property<int?>("BeatyandHealthyId");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("DiseaseName")
                        .IsRequired();

                    b.Property<string>("Image");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Protection");

                    b.Property<string>("Reasons");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.HasIndex("BeatyandHealthyId");

                    b.ToTable("Disease");
                });

            modelBuilder.Entity("MyDoctor.Models.DiseaseMedicin", b =>
                {
                    b.Property<int>("DiseaseId");

                    b.Property<int>("MedicinId");

                    b.Property<int>("Id");

                    b.HasKey("DiseaseId", "MedicinId");

                    b.HasIndex("MedicinId");

                    b.ToTable("DiseaseMedicin");
                });

            modelBuilder.Entity("MyDoctor.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("ConfirmPassword");

                    b.Property<string>("Country");

                    b.Property<string>("Email");

                    b.Property<string>("Kind");

                    b.Property<string>("Name");

                    b.Property<string>("Others");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Specials");

                    b.Property<string>("Telephone");

                    b.HasKey("Id");

                    b.ToTable("Doctor");
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
                });

            modelBuilder.Entity("MyDoctor.Models.Medicin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Affects");

                    b.Property<int>("BeatyandHealthyId");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("Image");

                    b.Property<string>("Indications");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("BeatyandHealthyId");

                    b.ToTable("Medicin");
                });

            modelBuilder.Entity("MyDoctor.Models.Posts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Department");

                    b.Property<string>("Description");

                    b.Property<int>("DoctorId");

                    b.Property<string>("Specific");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Posts");
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<int>("BeatyandHealthId");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("ImageOrVideo");

                    b.Property<DateTime?>("ModiteDate");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.HasIndex("BeatyandHealthId");

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

            modelBuilder.Entity("MyDoctor.Models.Disease", b =>
                {
                    b.HasOne("MyDoctor.Models.BeatyandHealthy", "BeatyandHealthy")
                        .WithMany("Diseases")
                        .HasForeignKey("BeatyandHealthyId");
                });

            modelBuilder.Entity("MyDoctor.Models.DiseaseMedicin", b =>
                {
                    b.HasOne("MyDoctor.Models.Disease", "Disease")
                        .WithMany("DiseaseMedicins")
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyDoctor.Models.Medicin", "Medicin")
                        .WithMany("DiseaseMedicins")
                        .HasForeignKey("MedicinId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyDoctor.Models.Medicin", b =>
                {
                    b.HasOne("MyDoctor.Models.BeatyandHealthy", "BeatyandHealthy")
                        .WithMany("Medicins")
                        .HasForeignKey("BeatyandHealthyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyDoctor.Models.Posts", b =>
                {
                    b.HasOne("MyDoctor.Models.Doctor", "Doctor")
                        .WithMany("Posts")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyDoctor.Models.RelativeofBeatyandhealthy", b =>
                {
                    b.HasOne("MyDoctor.Models.BeatyandHealthy", "BeatyandHealthy")
                        .WithMany("RelativeofBeatyandhealthies")
                        .HasForeignKey("BeatyandHealthId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
