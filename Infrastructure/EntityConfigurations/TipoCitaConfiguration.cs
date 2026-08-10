using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class TipoCitaConfiguration : IEntityTypeConfiguration<TipoCitaEntity>
{
    public void Configure(EntityTypeBuilder<TipoCitaEntity> builder)
    {
        builder.ToTable("tipo_cita");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Codigo).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
        builder.Property(x => x.DuracionMinutos).IsRequired();
        builder.Property(x => x.Activo).IsRequired();
        builder.HasIndex(x => x.Codigo).IsUnique();
    }
}
