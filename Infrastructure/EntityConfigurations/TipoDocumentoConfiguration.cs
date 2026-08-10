using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class TipoDocumentoConfiguration : IEntityTypeConfiguration<TipoDocumentoEntity>
{
    public void Configure(EntityTypeBuilder<TipoDocumentoEntity> builder)
    {
        builder.ToTable("tipo_documento");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Codigo).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
        builder.HasIndex(x => x.Codigo).IsUnique();
    }
}
