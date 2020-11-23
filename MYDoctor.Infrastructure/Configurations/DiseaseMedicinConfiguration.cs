using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MYDoctor.Core.Domain.Entities;
namespace MYDoctor.Infrastructure.Configurations
{
    public class DiseaseMedicinConfiguration : IEntityTypeConfiguration<DiseaseMedicin>
    {
        public void Configure(EntityTypeBuilder<DiseaseMedicin> builder)
        {
            builder.HasKey(bc => new { bc.DiseaseId, bc.MedicinId });
            builder.HasOne(bc => bc.Medicin)
                .WithMany(b => b.DiseaseMedicins)
                .HasForeignKey(bc => bc.MedicinId);
            builder.HasOne(bc => bc.Disease)
                .WithMany(c => c.DiseaseMedicins)
                .HasForeignKey(bc => bc.DiseaseId);
        }
    }
}
