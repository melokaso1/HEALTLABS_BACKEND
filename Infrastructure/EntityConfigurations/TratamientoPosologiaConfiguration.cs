using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class TratamientoPosologiaConfiguration : IEntityTypeConfiguration<TratamientoPosologiaEntity>
{
    public void Configure(EntityTypeBuilder<TratamientoPosologiaEntity> builder)
    {
        builder.ToTable("tratamiento_posologias");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TratamientoId).IsRequired();
        builder.Property(x => x.Dosis).HasMaxLength(100);
        builder.Property(x => x.Frecuencia).HasMaxLength(100);
        builder.Property(x => x.DuracionDias);
        builder.Property(x => x.Indicaciones).HasMaxLength(500);

        builder.HasIndex(x => x.TratamientoId).IsUnique();

        builder.HasOne(x => x.Tratamiento)
            .WithOne(t => t.Posologia)
            .HasForeignKey<TratamientoPosologiaEntity>(x => x.TratamientoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
