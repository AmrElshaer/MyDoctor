using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MYDoctor.Infrastructure.Identity;
namespace MYDoctor.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var admin = "Admin@Admin.com";
            builder.HasData(new ApplicationUser() { 
                 Id="1",
                 UserName=admin,
                 Email=admin,
                 PasswordHash=hasher.HashPassword(null,"Admin123@"),
                 NormalizedEmail=admin.ToUpper(),
                 NormalizedUserName=admin.ToUpper(),
                 SecurityStamp=System.Guid.NewGuid().ToString(),
                 LockoutEnabled=true
            });
        }
    }
}
