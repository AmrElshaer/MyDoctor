using System;
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
            #region Country
            builder.Entity<City>().HasOne(c=>c.Country).WithMany(country=>country.Cities).HasForeignKey(c=>c.CountryId);
            #endregion
           
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
            builder.Entity<Doctor>().HasOne(d => d.Category).WithMany(c => c.Doctors);
            builder
                .Entity<Doctor>()
                .Property(e => e.Kind)
                .HasConversion(converter);
            base.OnModelCreating(builder);
            
           
        }




        public DbSet<Disease> Disease { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<LikeandDislikeclass> LikeandDislikeclass { get; set; }
        public DbSet<BeatyandHealthy> BeatyandHealthy { get; set; }
        public DbSet<RelativeofBeatyandhealthy> RelativeofBeatyandhealthy { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Medicin> Medicin { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<MyDoctor.Models.City> Cities { get; set; }
    }
}
