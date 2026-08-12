using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class EmpleadoConfiguration : IEntityTypeConfiguration<EmpleadoEntity>
{
    public void Configure(EntityTypeBuilder<EmpleadoEntity> builder)
    {
        builder.ToTable("empleados");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PersonaId).IsRequired();
        builder.Property(x => x.CargoId).IsRequired();
        builder.Property(x => x.FechaIngreso).IsRequired();
        builder.Property(x => x.FechaRetiro);
        builder.Property(x => x.Activo).IsRequired();

        builder.HasIndex(x => x.PersonaId).IsUnique();

        builder.HasOne(x => x.Persona)
            .WithMany()
            .HasForeignKey(x => x.PersonaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Cargo)
            .WithMany()
            .HasForeignKey(x => x.CargoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
