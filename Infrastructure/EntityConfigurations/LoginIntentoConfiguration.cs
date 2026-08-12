using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class LoginIntentoConfiguration : IEntityTypeConfiguration<LoginIntentoEntity>
{
    public void Configure(EntityTypeBuilder<LoginIntentoEntity> builder)
    {
        builder.ToTable("login_intentos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UsuarioId);
        builder.Property(x => x.UsernameIntentado).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Exitoso).IsRequired();
        builder.Property(x => x.MotivoFallo).HasMaxLength(255);
        builder.Property(x => x.Ip).HasMaxLength(50);
        builder.Property(x => x.UserAgent).HasMaxLength(500);
        builder.Property(x => x.Fecha).IsRequired();

        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
