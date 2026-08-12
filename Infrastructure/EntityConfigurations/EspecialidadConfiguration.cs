using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class EspecialidadConfiguration : IEntityTypeConfiguration<EspecialidadEntity>
{
    public void Configure(EntityTypeBuilder<EspecialidadEntity> builder)
    {
        builder.ToTable("especialidades");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Descripcion).HasMaxLength(255);
        builder.HasIndex(x => x.Nombre).IsUnique();
    }
}
