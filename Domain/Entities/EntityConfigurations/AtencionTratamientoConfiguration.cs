namespace Domain.Entities.EntityConfigurations
{
    public class AtencionTratamientoConfig : IEntityTypeConfiguration<AtencionTratamientoEntity>
    {
        public void Configure(EntityTypeBuilder<AtencionTratamientoEntity> builder)
        {
            builder.ToTable("AtencionTratamiento");
            
            builder.Property(x => x.IdAtencionTratamiento).HasColumnName("id_atencion_tratamiento");
            builder.Property(x => x.IdDetalleCita).HasColumnName("id_detalle_cita").IsRequired();
            builder.Property(x => x.IdTratamiento).HasColumnName("id_tratamiento").IsRequired();
            builder.Property(x => x.DosisPersonalizada).HasColumnName("dosis_personalizada");
            builder.Property(x => x.FrecuenciaPersonalizada).HasColumnName("frecuencia_personalizada");
            builder.Property(x => x.DuracionDiasPersonalizada).HasColumnName("duracion_dias_personalizada");
            builder.Property(x => x.IndicacionesPersonalizadas).HasColumnName("indicaciones_personalizadas");

            builder.HasOne(x => x.DetalleCita).WithMany(x => x.AtencionesTratamiento).HasForeignKey(x => x.IdDetalleCita);
            builder.HasOne(x => x.Tratamiento).WithMany(x => x.AtencionesTratamiento).HasForeignKey(x => x.IdTratamiento);
        }
    }
    
}
