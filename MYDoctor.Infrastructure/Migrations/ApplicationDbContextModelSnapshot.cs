﻿// <auto-generated />
using System;
using MYDoctor.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MYDoctor.Infrastructure.Migrations
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

                    b.HasData(
                        new { Id = "1", ConcurrencyStamp = "59a7d461-9a8d-489f-9c18-8d540d05f04b", Name = "Admin", NormalizedName = "Admin" },
                        new { Id = "2", ConcurrencyStamp = "1c40eb6b-118d-4166-ad54-64fda1c4473b", Name = "Client", NormalizedName = "Client" },
                        new { Id = "3", ConcurrencyStamp = "0904a085-3220-48a3-991a-e182783a6a5a", Name = "Doctor", NormalizedName = "Doctor" }
                    );
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

                    b.HasData(
                        new { UserId = "1", RoleId = "1" }
                    );
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

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.BeatyandHealthy", b =>
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

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.Disease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BeatyandHealthyId");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("DiseaseName");

                    b.Property<string>("Image");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Protection");

                    b.Property<string>("Reasons");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.HasIndex("BeatyandHealthyId");

                    b.ToTable("Disease");
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.DiseaseMedicin", b =>
                {
                    b.Property<int>("DiseaseId");

                    b.Property<int>("MedicinId");

                    b.Property<int>("Id");

                    b.HasKey("DiseaseId", "MedicinId");

                    b.HasIndex("MedicinId");

                    b.ToTable("DiseaseMedicins");
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<string>("City");

                    b.Property<string>("ConfirmPassword");

                    b.Property<string>("Country");

                    b.Property<string>("Email");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Kind")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<string>("Others");

                    b.Property<string>("Password");

                    b.Property<string>("Telephone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.InboxMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsSee");

                    b.Property<int?>("ToUserProfileId");

                    b.Property<int?>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("ToUserProfileId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("InboxMessages");
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.Medicin", b =>
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

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("DisLike");

                    b.Property<int>("Like");

                    b.Property<int>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.RelativeofBeatyandhealthy", b =>
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

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.TableTrack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Action");

                    b.Property<string>("Content");

                    b.Property<string>("Controller");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Image");

                    b.Property<int>("ItemId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("TableTracks");
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.TableTrackUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LastUpdateSee");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("TableTrackUsers");
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");

                    b.HasData(
                        new { Id = 1, Email = "Admin@Admin.com", Name = "Admin@Admin.com" }
                    );
                });

            modelBuilder.Entity("MYDoctor.Infrastructure.Identity.ApplicationUser", b =>
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

                    b.HasData(
                        new { Id = "1", AccessFailedCount = 0, ConcurrencyStamp = "a99d3be4-473d-45e4-bc89-65a3924dc2d9", Email = "Admin@Admin.com", EmailConfirmed = false, LockoutEnabled = true, NormalizedEmail = "ADMIN@ADMIN.COM", NormalizedUserName = "ADMIN@ADMIN.COM", PasswordHash = "AQAAAAEAACcQAAAAEKFb4BLtOjHFSnwR9wQmU6Q08mfMo8luhLCai+lS2C5jBt1aTFZQjDDu1raGiqz6Nw==", PhoneNumberConfirmed = false, SecurityStamp = "4e294239-89e8-41da-8e81-077986330412", TwoFactorEnabled = false, UserName = "Admin@Admin.com" }
                    );
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
                    b.HasOne("MYDoctor.Infrastructure.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MYDoctor.Infrastructure.Identity.ApplicationUser")
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

                    b.HasOne("MYDoctor.Infrastructure.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MYDoctor.Infrastructure.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.City", b =>
                {
                    b.HasOne("MYDoctor.Core.Domain.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.Disease", b =>
                {
                    b.HasOne("MYDoctor.Core.Domain.Entities.BeatyandHealthy", "BeatyandHealthy")
                        .WithMany("Diseases")
                        .HasForeignKey("BeatyandHealthyId");
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.DiseaseMedicin", b =>
                {
                    b.HasOne("MYDoctor.Core.Domain.Entities.Disease", "Disease")
                        .WithMany("DiseaseMedicins")
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MYDoctor.Core.Domain.Entities.Medicin", "Medicin")
                        .WithMany("DiseaseMedicins")
                        .HasForeignKey("MedicinId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.Doctor", b =>
                {
                    b.HasOne("MYDoctor.Core.Domain.Entities.BeatyandHealthy", "Category")
                        .WithMany("Doctors")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.InboxMessage", b =>
                {
                    b.HasOne("MYDoctor.Core.Domain.Entities.UserProfile", "ToUserProfile")
                        .WithMany("ToMessagesInbox")
                        .HasForeignKey("ToUserProfileId");

                    b.HasOne("MYDoctor.Core.Domain.Entities.UserProfile", "UserProfile")
                        .WithMany("InboxMessages")
                        .HasForeignKey("UserProfileId");
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.Medicin", b =>
                {
                    b.HasOne("MYDoctor.Core.Domain.Entities.BeatyandHealthy", "BeatyandHealthy")
                        .WithMany("Medicins")
                        .HasForeignKey("BeatyandHealthyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.Post", b =>
                {
                    b.HasOne("MYDoctor.Core.Domain.Entities.BeatyandHealthy", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MYDoctor.Core.Domain.Entities.UserProfile", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MYDoctor.Core.Domain.Entities.RelativeofBeatyandhealthy", b =>
                {
                    b.HasOne("MYDoctor.Core.Domain.Entities.BeatyandHealthy", "BeatyandHealthy")
                        .WithMany("RelativeofBeatyandhealthies")
                        .HasForeignKey("BeatyandHealthId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
