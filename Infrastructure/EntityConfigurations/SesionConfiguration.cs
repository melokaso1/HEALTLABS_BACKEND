using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class SesionConfiguration : IEntityTypeConfiguration<SesionEntity>
{
    public void Configure(EntityTypeBuilder<SesionEntity> builder)
    {
        builder.ToTable("sesiones");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UsuarioId).IsRequired();
        builder.Property(x => x.Jti).IsRequired().HasMaxLength(100);
        builder.Property(x => x.RefreshTokenHash).IsRequired().HasMaxLength(255);
        builder.Property(x => x.AccessExpiresAt).IsRequired();
        builder.Property(x => x.RefreshExpiresAt).IsRequired();
        builder.Property(x => x.EmitidoEn).IsRequired();
        builder.Property(x => x.UltimoUso);
        builder.Property(x => x.RevocadoEn);
        builder.Property(x => x.MotivoRevocacion).HasMaxLength(255);
        builder.Property(x => x.Ip).HasMaxLength(50);
        builder.Property(x => x.UserAgent).HasMaxLength(500);
        builder.Property(x => x.Dispositivo).HasMaxLength(100);

        builder.HasIndex(x => x.Jti).IsUnique();
        builder.HasIndex(x => new { x.UsuarioId, x.RevocadoEn });

        builder.HasOne(x => x.Usuario)
            .WithMany(u => u.Sesiones)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
