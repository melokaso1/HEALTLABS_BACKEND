using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class TokenRecuperacionConfiguration : IEntityTypeConfiguration<TokenRecuperacionEntity>
{
    public void Configure(EntityTypeBuilder<TokenRecuperacionEntity> builder)
    {
        builder.ToTable("tokens_recuperacion");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UsuarioId).IsRequired();
        builder.Property(x => x.TokenHash).IsRequired().HasMaxLength(255);
        builder.Property(x => x.ExpiresAt).IsRequired();
        builder.Property(x => x.UsadoEn);
        builder.Property(x => x.IpSolicitud).HasMaxLength(50);
        builder.Property(x => x.FechaCreacion).IsRequired();

        builder.HasIndex(x => x.TokenHash).IsUnique();

        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
