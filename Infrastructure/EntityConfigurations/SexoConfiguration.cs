using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class SexoConfiguration : IEntityTypeConfiguration<SexoEntity>
{
    public void Configure(EntityTypeBuilder<SexoEntity> builder)
    {
        builder.ToTable("sexos");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Codigo).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(50);
        builder.HasIndex(x => x.Codigo).IsUnique();
    }
}
