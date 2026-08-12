using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class DiagnosticoConfiguration : IEntityTypeConfiguration<DiagnosticoEntity>
{
    public void Configure(EntityTypeBuilder<DiagnosticoEntity> builder)
    {
        builder.ToTable("diagnosticos");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CodigoCie10).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Activo).IsRequired();
        builder.HasIndex(x => x.CodigoCie10).IsUnique();
    }
}
