using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MYDoctor.Core.Application.Common.Enum;

namespace MYDoctor.Infrastructure.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole() { 
            Id="1",
            Name=Roles.Admin.ToString(),
            NormalizedName=Roles.Admin.ToString()
            },
            new IdentityRole()
            {
                Id = "2",
                Name = Roles.Client.ToString(),
                NormalizedName = Roles.Client.ToString()
            },
            new IdentityRole()
            {
                Id = "3",
                Name = Roles.Doctor.ToString(),
                NormalizedName = Roles.Doctor.ToString()
            }


            );
        }
    }
}
