using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFrameworkCore;

public class UsuarioConfig : IEntityTypeConfiguration<UsuarioEntity>
{
    public void Configure(EntityTypeBuilder<UsuarioEntity> builder)
    {
        builder.ToTable("usuario");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Email).IsRequired().HasMaxLength(150);
        builder.HasIndex(t => t.Email).IsUnique();
        builder.Property(t => t.Password).IsRequired().HasMaxLength(255);
        builder.Property(t => t.Activo).IsRequired();
        builder.Property(t => t.JwtCode);

        builder.HasOne<RolEntity>()
            .WithMany()
            .HasForeignKey(t => t.RolId);
    }
}
