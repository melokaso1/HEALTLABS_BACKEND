using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class MedicoConfiguration : IEntityTypeConfiguration<MedicoEntity>
{
    public void Configure(EntityTypeBuilder<MedicoEntity> builder)
    {
        builder.ToTable("medicos");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.EmpleadoId).IsRequired();
        builder.Property(x => x.RegistroProfesional).HasMaxLength(100);
        builder.Property(x => x.Activo).IsRequired();

        builder.HasIndex(x => x.EmpleadoId).IsUnique();

        builder.HasOne(x => x.Empleado)
            .WithOne(e => e.Medico)
            .HasForeignKey<MedicoEntity>(x => x.EmpleadoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
