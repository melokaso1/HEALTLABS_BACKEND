using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class PersonaDireccionConfiguration : IEntityTypeConfiguration<PersonaDireccionEntity>
{
    public void Configure(EntityTypeBuilder<PersonaDireccionEntity> builder)
    {
        builder.ToTable("persona_direccion", t =>
            t.HasCheckConstraint(
                "CK_persona_direccion_tipo",
                "\"Tipo\" IS NULL OR \"Tipo\" IN ('residencia', 'trabajo')"));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.PersonaId).IsRequired();
        builder.Property(x => x.Direccion).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Ciudad).HasMaxLength(100);
        builder.Property(x => x.Tipo).HasMaxLength(20);
        builder.Property(x => x.Principal).IsRequired();

        builder.HasIndex(x => x.PersonaId)
            .IsUnique()
            .HasFilter("\"Principal\" = TRUE")
            .HasDatabaseName("UX_persona_direccion_principal");

        builder.HasOne(x => x.Persona)
            .WithMany(p => p.Direcciones)
            .HasForeignKey(x => x.PersonaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
