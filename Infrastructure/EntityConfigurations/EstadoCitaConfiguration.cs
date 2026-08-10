using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class EstadoCitaConfiguration : IEntityTypeConfiguration<EstadoCitaEntity>
{
    public void Configure(EntityTypeBuilder<EstadoCitaEntity> builder)
    {
        builder.ToTable("estado_cita");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Codigo).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Descripcion).HasMaxLength(255);
        builder.HasIndex(x => x.Codigo).IsUnique();
    }
}
