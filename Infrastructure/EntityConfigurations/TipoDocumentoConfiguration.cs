using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFrameworkCore;

public class TipoDocumentoConfig : IEntityTypeConfiguration<TipoDocumentoEntity>
{
    public void Configure(EntityTypeBuilder<TipoDocumentoEntity> builder)
    {
        builder.ToTable("tipo_documento");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Nombre).IsRequired().HasMaxLength(100);
    }
}
