namespace Domain.Entities
{
    public class AtencionTratamientoEntity
    {
        public Guid Id { get; set; }
        public Guid DetalleCitaId { get; set; }
        public Guid TratamientoId { get; set; }
        public string Dosis { get; set; } = null!;
        public string? Frecuencia { get; set; }
        public int? DuracionDias { get; set; }
        public string? Indicaciones { get; set; }

        public DetalleCitaEntity? DetalleCita { get; set; }
        public TratamientoEntity? Tratamiento { get; set; }

        private AtencionTratamientoEntity() { }

        public AtencionTratamientoEntity(
            Guid detalleCitaId,
            Guid tratamientoId,
            string dosis,
            string? frecuencia,
            int? duracionDias,
            string? indicaciones)
        {
            Id = Guid.NewGuid();
            DetalleCitaId = detalleCitaId;
            TratamientoId = tratamientoId;
            Dosis = dosis;
            Frecuencia = frecuencia;
            DuracionDias = duracionDias;
            Indicaciones = indicaciones;
        }
    }
}
