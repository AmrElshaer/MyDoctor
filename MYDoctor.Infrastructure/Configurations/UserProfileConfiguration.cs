using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MYDoctor.Core.Domain.Entities;
namespace MYDoctor.Infrastructure.Configurations
{
    class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            var admin = "Admin@Admin.com";
            builder.HasData(new UserProfile()
            {
                Id = 1,
                Name = admin,
                Email = admin,
            });
          
        }
    }
}
