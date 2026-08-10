namespace Domain.Entities
{
    public class AtencionTratamientoEntity
    {
        public Guid Id { get; set; }
        public int DetalleCitaId { get; set; }
        public int TratamientoId { get; set; }
        public string Dosis { get; set; } = null!;
        public string? Frecuencia { get; set; } = null!;
        public int? DuracionDias { get; set; }
        public string? Indicaciones { get; set; }

        private AtencionTratamientoEntity() { }

        public AtencionTratamientoEntity(int DetalleCita, int TratamientoId, string dosis, string frecuencia, int? duracionDias, string? indicaciones)
        {
            Id = Guid.NewGuid();
            DetalleCitaId = DetalleCita;
            Dosis = dosis;
            Frecuencia = frecuencia;
            DuracionDias = duracionDias;
            Indicaciones = indicaciones;
        }

        public void update(int DetalleCita, int TratamientoId, string dosis, string frecuencia, int? duracionDias, string? indicaciones)
        {
            DetalleCitaId = DetalleCita;
            Dosis = dosis;
            Frecuencia = frecuencia;
            DuracionDias = duracionDias;
            Indicaciones = indicaciones;
        }
    }
}
