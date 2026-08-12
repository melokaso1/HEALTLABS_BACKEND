using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class MedicoEspecialidadConfiguration : IEntityTypeConfiguration<MedicoEspecialidadEntity>
{
    public void Configure(EntityTypeBuilder<MedicoEspecialidadEntity> builder)
    {
        builder.ToTable("medico_especialidades");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.MedicoId).IsRequired();
        builder.Property(x => x.EspecialidadId).IsRequired();
        builder.Property(x => x.Principal).IsRequired();

        builder.HasIndex(x => new { x.MedicoId, x.EspecialidadId }).IsUnique();
        builder.HasIndex(x => x.MedicoId)
            .IsUnique()
            .HasFilter("\"Principal\" = TRUE")
            .HasDatabaseName("UX_medico_especialidades_principal");

        builder.HasOne(x => x.Medico)
            .WithMany(m => m.Especialidades)
            .HasForeignKey(x => x.MedicoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Especialidad)
            .WithMany()
            .HasForeignKey(x => x.EspecialidadId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
