using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MYDoctor.Core.Domain.Entities;
using System;
namespace MYDoctor.Infrastructure.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasOne(p=>p.Category).WithMany(c=>c.Posts).HasForeignKey(p=>p.CategoryId);
            builder.HasOne(p=>p.User).WithMany(c=>c.Posts).HasForeignKey(p=>p.UserProfileId);

        }
    }
}
