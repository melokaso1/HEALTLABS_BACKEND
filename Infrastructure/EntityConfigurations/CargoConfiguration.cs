using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.EntityConfigurations
{
    public class CargoConfig : IEntityTypeConfiguration<CargoEntity>
    {
        public void Configure(EntityTypeBuilder<CargoEntity> builder)
        {
            builder.ToTable("cargo");
            builder.HasKey(x => x.IdCargo);

            builder.Property(x => x.IdCargo).HasColumnName("id");
            builder.Property(x => x.Nombre).HasColumnName("nombre").IsRequired();
            builder.Property(x => x.Codigo).HasColumnName("codigo");
            builder.Property(x => x.Descripcion).HasColumnName("descripcion");
            builder.Property(x => x.NivelJerarquico).HasColumnName("nivel_jerarquico");
        }
    }
}
