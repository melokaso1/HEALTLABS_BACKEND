using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class PersonaConfiguration : IEntityTypeConfiguration<PersonaEntity>
{
    public void Configure(EntityTypeBuilder<PersonaEntity> builder)
    {
        builder.ToTable("personas");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Apellido).IsRequired().HasMaxLength(100);
        builder.Property(x => x.TipoDocumentoId).IsRequired();
        builder.Property(x => x.NumeroDocumento).IsRequired().HasMaxLength(30);
        builder.Property(x => x.FechaNacimiento);
        builder.Property(x => x.SexoId);
        builder.Property(x => x.FechaCreacion).IsRequired();

        builder.HasIndex(x => new { x.TipoDocumentoId, x.NumeroDocumento }).IsUnique();

        builder.HasOne(x => x.TipoDocumento)
            .WithMany()
            .HasForeignKey(x => x.TipoDocumentoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Sexo)
            .WithMany()
            .HasForeignKey(x => x.SexoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
