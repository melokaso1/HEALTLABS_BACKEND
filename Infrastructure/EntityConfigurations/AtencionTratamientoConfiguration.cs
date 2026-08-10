using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class AtencionTratamientoConfiguration : IEntityTypeConfiguration<AtencionTratamientoEntity>
{
    public void Configure(EntityTypeBuilder<AtencionTratamientoEntity> builder)
    {
        builder.ToTable("atencion_tratamiento");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DetalleCitaId).IsRequired();
        builder.Property(x => x.TratamientoId).IsRequired();
        builder.Property(x => x.Dosis).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Frecuencia).HasMaxLength(100);
        builder.Property(x => x.DuracionDias);
        builder.Property(x => x.Indicaciones).HasMaxLength(500);

        builder.HasOne(x => x.DetalleCita)
            .WithMany(d => d.AtencionesTratamiento)
            .HasForeignKey(x => x.DetalleCitaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Tratamiento)
            .WithMany()
            .HasForeignKey(x => x.TratamientoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
