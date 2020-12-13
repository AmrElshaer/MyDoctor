using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Infrastructure.Configurations
{
    public class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessage>
    {
        public void Configure(EntityTypeBuilder<InboxMessage> builder)
        {
            builder.HasOne(m => m.UserProfile).WithMany(u => u.InboxMessages)
            .HasForeignKey(m => m.UserProfileId);
            builder.HasOne(m => m.ToUserProfile).WithMany(u => u.ToMessagesInbox)
           .HasForeignKey(m => m.ToUserProfileId);
        }
    }
}
