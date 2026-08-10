using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFrameworkCore;

public class AtencionTratamientoConfig : IEntityTypeConfiguration<AtencionTratamientoEntity>
{
    public void Configure(EntityTypeBuilder<AtencionTratamientoEntity> builder)
    {
        builder.ToTable("atencion_tratamiento");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.DetalleCitaId).IsRequired();
        builder.Property(t => t.TratamientoId).IsRequired();
        builder.Property(t => t.Dosis).IsRequired();
        builder.Property(t => t.Frecuencia).IsRequired();
        builder.Property(t => t.DuracionDias);
        builder.Property(t => t.Indicaciones).HasMaxLength(500);
    }
}

