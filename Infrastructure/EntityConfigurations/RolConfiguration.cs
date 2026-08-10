using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFrameworkCore;

public class RolConfig : IEntityTypeConfiguration<RolEntity>
{
    public void Configure(EntityTypeBuilder<RolEntity> builder)
    {
        builder.ToTable("rol");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.NombreRol).IsRequired().HasMaxLength(100);
        builder.Property(t => t.Descripcion).HasMaxLength(255);
    }
}
