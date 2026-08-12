using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<UsuarioEntity>
{
    public void Configure(EntityTypeBuilder<UsuarioEntity> builder)
    {
        builder.ToTable("usuarios");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.EmpleadoId).IsRequired();
        builder.Property(x => x.RolId).IsRequired();
        builder.Property(x => x.Username).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
        builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Activo).IsRequired();
        builder.Property(x => x.FechaCreacion).IsRequired();
        builder.Property(x => x.UltimoLogin);
        builder.Property(x => x.IntentosFallidos).IsRequired();
        builder.Property(x => x.BloqueadoHasta);
        builder.Property(x => x.DebeCambiarPassword).IsRequired();
        builder.Property(x => x.PasswordChangedAt);
        builder.Property(x => x.TokenVersion).IsRequired();

        builder.HasIndex(x => x.Username).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.EmpleadoId).IsUnique();

        builder.HasOne(x => x.Empleado)
            .WithOne(e => e.Usuario)
            .HasForeignKey<UsuarioEntity>(x => x.EmpleadoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Rol)
            .WithMany()
            .HasForeignKey(x => x.RolId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
