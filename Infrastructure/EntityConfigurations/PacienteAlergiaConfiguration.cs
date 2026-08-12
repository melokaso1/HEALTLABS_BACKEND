using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class PacienteAlergiaConfiguration : IEntityTypeConfiguration<PacienteAlergiaEntity>
{
    public void Configure(EntityTypeBuilder<PacienteAlergiaEntity> builder)
    {
        builder.ToTable("paciente_alergias", t =>
            t.HasCheckConstraint(
                "CK_paciente_alergias_severidad",
                "\"Severidad\" IS NULL OR \"Severidad\" IN ('leve', 'moderada', 'severa')"));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.PacienteId).IsRequired();
        builder.Property(x => x.Sustancia).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Reaccion).HasMaxLength(500);
        builder.Property(x => x.Severidad).HasMaxLength(20);
        builder.Property(x => x.Activo).IsRequired();
        builder.Property(x => x.FechaRegistro).IsRequired();

        builder.HasOne(x => x.Paciente)
            .WithMany(p => p.Alergias)
            .HasForeignKey(x => x.PacienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
