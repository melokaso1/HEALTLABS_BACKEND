using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class CargoConfiguration : IEntityTypeConfiguration<CargoEntity>
{
    public void Configure(EntityTypeBuilder<CargoEntity> builder)
    {
        builder.ToTable("cargo");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Codigo).HasMaxLength(50);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Descripcion).HasMaxLength(255);
        builder.Property(x => x.NivelJerarquico);
    }
}
