using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class AntecedenteConfiguration : IEntityTypeConfiguration<AntecedenteEntity>
{
    public void Configure(EntityTypeBuilder<AntecedenteEntity> builder)
    {
        builder.ToTable("antecedentes", t =>
            t.HasCheckConstraint(
                "CK_antecedentes_tipo",
                "\"Tipo\" IN ('personal', 'familiar', 'quirurgico')"));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.PacienteId).IsRequired();
        builder.Property(x => x.Tipo).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.FechaRegistro).IsRequired();
        builder.Property(x => x.UsuarioRegistroId);

        builder.HasOne(x => x.Paciente)
            .WithMany(p => p.Antecedentes)
            .HasForeignKey(x => x.PacienteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.UsuarioRegistro)
            .WithMany()
            .HasForeignKey(x => x.UsuarioRegistroId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
