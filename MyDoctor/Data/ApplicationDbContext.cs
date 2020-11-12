﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyDoctor.Helper;
using MyDoctor.Models;

namespace MyDoctor.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
     

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
      
        protected override void OnModelCreating(ModelBuilder builder)
        {

            // Add Relation For Doctor and posts
            builder.Entity<Posts>().HasOne<Doctor>(s => s.Doctor).WithMany(g => g.Posts).HasForeignKey(s => s.DoctorId);
            // Add Relation For Category and RelativeCategory
            builder.Entity<RelativeofBeatyandhealthy>()
                .HasOne<BeatyandHealthy>(relativeBeaty => relativeBeaty.BeatyandHealthy)
                .WithMany(category => category.RelativeofBeatyandhealthies)
                .HasForeignKey(relativebeaty => relativebeaty.BeatyandHealthId);
            builder.Entity<DiseaseMedicin>()
                .HasKey(bc => new { bc.DiseaseId, bc.MedicinId });
            builder.Entity<DiseaseMedicin>()
                .HasOne(bc => bc.Medicin)
                .WithMany(b => b.DiseaseMedicins)
                .HasForeignKey(bc => bc.MedicinId);
            builder.Entity<DiseaseMedicin>()
                .HasOne(bc => bc.Disease)
                .WithMany(c => c.DiseaseMedicins)
                .HasForeignKey(bc => bc.DiseaseId);
            builder.Entity<Medicin>().HasOne(m => m.BeatyandHealthy).WithMany(b => b.Medicins)
                .HasForeignKey(m => m.BeatyandHealthyId);
            builder.Entity<Disease>().HasOne(m => m.BeatyandHealthy).WithMany(b => b.Diseases)
                .HasForeignKey(m => m.BeatyandHealthyId);
            //Value Converters string when insert enum when write
            var converter = new ValueConverter<Kind, string>(
                v => v.ToString(),
                v => (Kind)Enum.Parse(typeof(Kind), v));

            builder
                .Entity<Doctor>()
                .Property(e => e.Kind)
                .HasConversion(converter);
            base.OnModelCreating(builder);
            
           
        }




        public DbSet<MyDoctor.Models.Disease> Disease { get; set; }
        public DbSet<MyDoctor.Models.Comments> Comments { get; set; }
        public DbSet<MyDoctor.Models.LikeandDislikeclass> LikeandDislikeclass { get; set; }
        public DbSet<MyDoctor.Models.RelativeDisease> RelativeDisease { get; set; }
        public DbSet<MyDoctor.Models.BeatyandHealthy> BeatyandHealthy { get; set; }
        public DbSet<MyDoctor.Models.RelativeofBeatyandhealthy> RelativeofBeatyandhealthy { get; set; }
        public DbSet<MyDoctor.Models.Doctor> Doctor { get; set; }
        public DbSet<MyDoctor.Models.Posts> Posts { get; set; }
        public DbSet<MyDoctor.Models.Commentfordoctorpost> Commentfordoctorpost { get; set; }
        public DbSet<MyDoctor.Models.Medicin> Medicin { get; set; }
    }
}
