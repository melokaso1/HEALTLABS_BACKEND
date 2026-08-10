using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class TratamientoConfiguration : IEntityTypeConfiguration<TratamientoEntity>
{
    public void Configure(EntityTypeBuilder<TratamientoEntity> builder)
    {
        builder.ToTable("tratamiento");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Codigo).HasMaxLength(50);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Descripcion).HasMaxLength(500);
        builder.Property(x => x.Activo).IsRequired();
        builder.HasIndex(x => x.Codigo).IsUnique();
    }
}
