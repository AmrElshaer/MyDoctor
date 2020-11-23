using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Infrastructure.Configurations
{
    public class MedicinConfiguration : IEntityTypeConfiguration<Medicin>
    {
        public void Configure(EntityTypeBuilder<Medicin> builder)
        {
            builder.HasOne(m => m.BeatyandHealthy).WithMany(b => b.Medicins)
                .HasForeignKey(m => m.BeatyandHealthyId);
        }
    }
}
