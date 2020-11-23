using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MYDoctor.Core.Domain.Common.Enum;
using MYDoctor.Core.Domain.Entities;
using System;
namespace MYDoctor.Infrastructure.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            
            var converter = new ValueConverter<Kind, string>(
             v => v.ToString(),
             v => (Kind)Enum.Parse(typeof(Kind), v));
            builder.HasOne(d => d.Category).WithMany(c => c.Doctors);
            builder.Property(e => e.Kind).HasConversion(converter);
        }
    }
}
