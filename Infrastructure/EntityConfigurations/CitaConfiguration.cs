using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class CitaConfiguration : IEntityTypeConfiguration<CitaEntity>
{
    public void Configure(EntityTypeBuilder<CitaEntity> builder)
    {
        builder.ToTable("citas");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PacienteId).IsRequired();
        builder.Property(x => x.MedicoId).IsRequired();
        builder.Property(x => x.EstadoCitaId).IsRequired();
        builder.Property(x => x.TipoCitaId).IsRequired();
        builder.Property(x => x.Fecha).IsRequired();
        builder.Property(x => x.HoraInicio).IsRequired();
        builder.Property(x => x.HoraFin).IsRequired();
        builder.Property(x => x.MotivoConsulta).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Observaciones).HasMaxLength(1000);
        builder.Property(x => x.UsuarioCreacionId).IsRequired();
        builder.Property(x => x.FechaCreacion).IsRequired();
        builder.Property(x => x.MotivoCancelacion).HasMaxLength(500);
        builder.Property(x => x.UsuarioCancelacionId);
        builder.Property(x => x.FechaCancelacion);

        builder.HasIndex(x => new { x.MedicoId, x.Fecha, x.HoraInicio });
        builder.HasIndex(x => new { x.PacienteId, x.Fecha });

        builder.HasOne(x => x.Paciente)
            .WithMany()
            .HasForeignKey(x => x.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Medico)
            .WithMany()
            .HasForeignKey(x => x.MedicoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.EstadoCita)
            .WithMany()
            .HasForeignKey(x => x.EstadoCitaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.TipoCita)
            .WithMany()
            .HasForeignKey(x => x.TipoCitaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.UsuarioCreacion)
            .WithMany()
            .HasForeignKey(x => x.UsuarioCreacionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.UsuarioCancelacion)
            .WithMany()
            .HasForeignKey(x => x.UsuarioCancelacionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
