using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class PacienteConfiguration : IEntityTypeConfiguration<PacienteEntity>
{
    public void Configure(EntityTypeBuilder<PacienteEntity> builder)
    {
        builder.ToTable("paciente");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PersonaId).IsRequired();
        builder.Property(x => x.Activo).IsRequired();
        builder.Property(x => x.FechaRegistro).IsRequired();

        builder.HasIndex(x => x.PersonaId).IsUnique();

        builder.HasOne(x => x.Persona)
            .WithMany()
            .HasForeignKey(x => x.PersonaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
