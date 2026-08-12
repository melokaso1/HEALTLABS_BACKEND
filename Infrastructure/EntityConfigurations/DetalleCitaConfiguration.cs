using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class DetalleCitaConfiguration : IEntityTypeConfiguration<DetalleCitaEntity>
{
    public void Configure(EntityTypeBuilder<DetalleCitaEntity> builder)
    {
        builder.ToTable("detalles_cita");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CitaId).IsRequired();
        builder.Property(x => x.MedicoId).IsRequired();
        builder.Property(x => x.NotaAtencion).HasMaxLength(2000);
        builder.Property(x => x.ResumenConsulta).HasMaxLength(2000);
        builder.Property(x => x.FechaRegistro).IsRequired();

        builder.HasIndex(x => x.CitaId).IsUnique();

        builder.HasOne(x => x.Cita)
            .WithOne(c => c.DetalleCita)
            .HasForeignKey<DetalleCitaEntity>(x => x.CitaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Medico)
            .WithMany()
            .HasForeignKey(x => x.MedicoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
