using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class PermisoConfiguration : IEntityTypeConfiguration<PermisoEntity>
{
    public void Configure(EntityTypeBuilder<PermisoEntity> builder)
    {
        builder.ToTable("permiso");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Codigo).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Modulo).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Descripcion).HasMaxLength(255);
        builder.HasIndex(x => x.Codigo).IsUnique();
    }
}
