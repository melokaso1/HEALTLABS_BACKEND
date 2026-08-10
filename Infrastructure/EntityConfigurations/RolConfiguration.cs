using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class RolConfiguration : IEntityTypeConfiguration<RolEntity>
{
    public void Configure(EntityTypeBuilder<RolEntity> builder)
    {
        builder.ToTable("rol");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.NombreRol).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Descripcion).HasMaxLength(255);
        builder.Property(x => x.Activo).IsRequired();
        builder.HasIndex(x => x.NombreRol).IsUnique();
    }
}
