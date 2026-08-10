using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class CitaHistorialEstadoConfiguration : IEntityTypeConfiguration<CitaHistorialEstadoEntity>
{
    public void Configure(EntityTypeBuilder<CitaHistorialEstadoEntity> builder)
    {
        builder.ToTable("cita_historial_estado");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CitaId).IsRequired();
        builder.Property(x => x.EstadoAnteriorId).IsRequired();
        builder.Property(x => x.EstadoNuevoId).IsRequired();
        builder.Property(x => x.UsuarioId);
        builder.Property(x => x.FechaCambio).IsRequired();
        builder.Property(x => x.Observacion).HasMaxLength(500);

        builder.HasOne(x => x.Cita)
            .WithMany(c => c.HistorialEstados)
            .HasForeignKey(x => x.CitaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.EstadoAnterior)
            .WithMany()
            .HasForeignKey(x => x.EstadoAnteriorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.EstadoNuevo)
            .WithMany()
            .HasForeignKey(x => x.EstadoNuevoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
