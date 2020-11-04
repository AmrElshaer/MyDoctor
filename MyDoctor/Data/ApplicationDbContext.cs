using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Helper;
using MyDoctor.Models;

namespace MyDoctor.Data
{
    public class ApplicationDbContext : IdentityDbContext<CutomPropertiy>
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
            base.OnModelCreating(builder);
            //SeedData.AddBeatyandHealthy(builder);
            //SeedData.AddLikeAddDisLike(builder);
           
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
