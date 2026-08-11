using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class HorarioConfiguration : IEntityTypeConfiguration<HorarioEntity>
{
    public void Configure(EntityTypeBuilder<HorarioEntity> builder)
    {
        builder.ToTable("horario", t =>
            t.HasCheckConstraint(
                "CK_horario_jornada",
                "\"HoraEntrada\" < \"SalidaAlmuerzo\" AND \"SalidaAlmuerzo\" < \"RetornoActividades\" AND \"RetornoActividades\" < \"HoraSalida\""));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.MedicoId).IsRequired();
        builder.Property(x => x.Fecha).IsRequired();
        builder.Property(x => x.HoraEntrada).IsRequired();
        builder.Property(x => x.HoraSalida).IsRequired();
        builder.Property(x => x.SalidaAlmuerzo).IsRequired();
        builder.Property(x => x.RetornoActividades).IsRequired();

        builder.HasIndex(x => new { x.MedicoId, x.Fecha }).IsUnique();

        builder.HasOne(x => x.Medico)
            .WithMany(m => m.Horarios)
            .HasForeignKey(x => x.MedicoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
