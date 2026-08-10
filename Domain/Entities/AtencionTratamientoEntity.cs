namespace Domain.Entities
{
    public class AtencionTratamientoEntity
    {
        public Guid Id { get; private set; }
        public Guid DetalleCitaId { get; private set; }
        public Guid TratamientoId { get; private set; }
        public string Dosis { get; private set; } = null!;
        public string? Frecuencia { get; private set; }
        public int? DuracionDias { get; private set; }
        public string? Indicaciones { get; private set; }

        private AtencionTratamientoEntity() { }
        public AtencionTratamientoEntity(Guid detalleCitaId, Guid tratamientoId, string dosis, string? frecuencia, int? duracionDias, string? indicaciones)
        {
            Id = Guid.NewGuid();
            DetalleCitaId = detalleCitaId;
            TratamientoId = tratamientoId; // <-- ¡Faltaba esta línea!
            Dosis = dosis;
            Frecuencia = frecuencia;
            DuracionDias = duracionDias;
            Indicaciones = indicaciones;
        }

        public void Update(Guid detalleCitaId, Guid tratamientoId, string dosis, string? frecuencia, int? duracionDias, string? indicaciones)
        {
            DetalleCitaId = detalleCitaId;
            TratamientoId = tratamientoId; // <-- ¡Faltaba esta línea!
            Dosis = dosis;
            Frecuencia = frecuencia;
            DuracionDias = duracionDias;
            Indicaciones = indicaciones;
        }
    }
}