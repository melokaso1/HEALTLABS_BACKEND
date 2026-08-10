using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class PersonaTelefonoConfiguration : IEntityTypeConfiguration<PersonaTelefonoEntity>
{
    public void Configure(EntityTypeBuilder<PersonaTelefonoEntity> builder)
    {
        builder.ToTable("persona_telefono", t =>
            t.HasCheckConstraint(
                "CK_persona_telefono_tipo",
                "Tipo IS NULL OR Tipo IN ('movil', 'fijo', 'trabajo')"));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.PersonaId).IsRequired();
        builder.Property(x => x.Telefono).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Tipo).HasMaxLength(20);
        builder.Property(x => x.Principal).IsRequired();

        builder.HasIndex(x => x.PersonaId)
            .IsUnique()
            .HasFilter("Principal = 1")
            .HasDatabaseName("UX_persona_telefono_principal");

        builder.HasOne(x => x.Persona)
            .WithMany(p => p.Telefonos)
            .HasForeignKey(x => x.PersonaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
