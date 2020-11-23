﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Configurations;

namespace MYDoctor.Infrastructure.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
     

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new DiseaseConfiguration());
            builder.ApplyConfiguration(new DoctorConfiguration());
            builder.ApplyConfiguration(new MedicinConfiguration());
            builder.ApplyConfiguration(new RelativeofBeatyandhealthyConfiguration());
            builder.ApplyConfiguration(new DiseaseMedicinConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            base.OnModelCreating(builder);
        }
        public DbSet<Disease> Disease { get; set; }
        public DbSet<BeatyandHealthy> BeatyandHealthy { get; set; }
        public DbSet<RelativeofBeatyandhealthy> RelativeofBeatyandhealthy { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Medicin> Medicin { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<DiseaseMedicin> DiseaseMedicins { get; set; }
    }
}