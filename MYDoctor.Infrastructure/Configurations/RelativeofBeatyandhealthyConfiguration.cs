using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Infrastructure.Configurations
{
    public class RelativeofBeatyandhealthyConfiguration : IEntityTypeConfiguration<RelativeofBeatyandhealthy>
    {
        public void Configure(EntityTypeBuilder<RelativeofBeatyandhealthy> builder)
        {
            builder.HasOne<BeatyandHealthy>(relativeBeaty => relativeBeaty.BeatyandHealthy)
                .WithMany(category => category.RelativeofBeatyandhealthies)
                .HasForeignKey(relativebeaty => relativebeaty.BeatyandHealthId);
        }
    }
}
