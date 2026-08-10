using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class RolPermisoConfiguration : IEntityTypeConfiguration<RolPermisoEntity>
{
    public void Configure(EntityTypeBuilder<RolPermisoEntity> builder)
    {
        builder.ToTable("rol_permiso");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.RolId).IsRequired();
        builder.Property(x => x.PermisoId).IsRequired();

        builder.HasIndex(x => new { x.RolId, x.PermisoId }).IsUnique();

        builder.HasOne(x => x.Rol)
            .WithMany(r => r.RolPermisos)
            .HasForeignKey(x => x.RolId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Permiso)
            .WithMany(p => p.RolPermisos)
            .HasForeignKey(x => x.PermisoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
