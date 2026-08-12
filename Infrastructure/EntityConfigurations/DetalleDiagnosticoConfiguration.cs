using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class DetalleDiagnosticoConfiguration : IEntityTypeConfiguration<DetalleDiagnosticoEntity>
{
    public void Configure(EntityTypeBuilder<DetalleDiagnosticoEntity> builder)
    {
        builder.ToTable("detalles_diagnostico");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.DetalleCitaId).IsRequired();
        builder.Property(x => x.DiagnosticoId).IsRequired();
        builder.Property(x => x.Principal).IsRequired();

        builder.HasIndex(x => new { x.DetalleCitaId, x.DiagnosticoId }).IsUnique();
        builder.HasIndex(x => x.DetalleCitaId)
            .IsUnique()
            .HasFilter("\"Principal\" = TRUE")
            .HasDatabaseName("UX_detalles_diagnostico_principal");

        builder.HasOne(x => x.DetalleCita)
            .WithMany(d => d.DetallesDiagnostico)
            .HasForeignKey(x => x.DetalleCitaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Diagnostico)
            .WithMany()
            .HasForeignKey(x => x.DiagnosticoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
