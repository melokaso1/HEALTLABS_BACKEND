using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFrameworkCore;

public class PersonaConfig : IEntityTypeConfiguration<PersonaEntity>
{
    public void Configure(EntityTypeBuilder<PersonaEntity> builder)
    {
        builder.ToTable("personas");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Nombre).IsRequired().HasMaxLength(100);
        builder.Property(t => t.Apellido).IsRequired().HasMaxLength(100);
        builder.Property(t => t.TipoDocumentoId);
        builder.Property(t => t.NumeroDocumento).IsRequired().HasMaxLength(50);
        builder.Property(t => t.Direccion).HasMaxLength(255);
        builder.Property(t => t.Telefono).HasMaxLength(50);
        builder.Property(t => t.FechaNacimiento);
        builder.Property(t => t.Sexo).HasMaxLength(20);
    }
}
