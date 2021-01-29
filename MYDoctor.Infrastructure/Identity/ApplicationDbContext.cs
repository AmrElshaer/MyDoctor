using Microsoft.AspNetCore.Identity;
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

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new DiseaseConfiguration());
            builder.ApplyConfiguration(new DoctorConfiguration());
            builder.ApplyConfiguration(new MedicinConfiguration());
            builder.ApplyConfiguration(new RelativeofBeatyandhealthyConfiguration());
            builder.ApplyConfiguration(new DiseaseMedicinConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new InboxMessageConfiguration());
            builder.ApplyConfiguration(new PostConfiguration());
            builder.Entity<UserProfile>().HasData(new UserProfile()
            {
                Id = 1,
                Email = "Admin@Admin.com",
                Name = "Admin@Admin.com"
            })
            
            ;
            base.OnModelCreating(builder);
        }
        public DbSet<Disease> Disease { get; set; }
        public DbSet<BeatyandHealthy> BeatyandHealthy { get; set; }
        public DbSet<RelativeofBeatyandhealthy> RelativeofBeatyandhealthy { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Medicin> Medicin { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<TableTrack> TableTracks { get; set; }
        public DbSet<DiseaseMedicin> DiseaseMedicins { get; set; }
        public DbSet<TableTrackUser> TableTrackUsers { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<InboxMessage> InboxMessages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<DisLike> DisLikes { get; set; }
    }
}
