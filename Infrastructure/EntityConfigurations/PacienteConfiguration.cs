using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class PacienteConfiguration : IEntityTypeConfiguration<PacienteEntity>
{
    public void Configure(EntityTypeBuilder<PacienteEntity> builder)
    {
        builder.ToTable("pacientes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PersonaId).IsRequired();
        builder.Property(x => x.Activo).IsRequired();
        builder.Property(x => x.TipoSangre).HasMaxLength(3);
        builder.Property(x => x.FechaRegistro).IsRequired();

        builder.HasIndex(x => x.PersonaId).IsUnique();
        builder.ToTable("pacientes", table =>
            table.HasCheckConstraint(
                "CK_pacientes_tipo_sangre",
                "\"TipoSangre\" IS NULL OR \"TipoSangre\" IN ('A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-')"));

        builder.HasOne(x => x.Persona)
            .WithMany()
            .HasForeignKey(x => x.PersonaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
