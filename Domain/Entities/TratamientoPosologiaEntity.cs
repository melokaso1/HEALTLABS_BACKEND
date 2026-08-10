namespace Domain.Entities
{
    public class TratamientoPosologiaEntity
    {
        public Guid Id { get; set; }
        public Guid TratamientoId { get; set; }
        public string? Dosis { get; set; }
        public string? Frecuencia { get; set; }
        public int? DuracionDias { get; set; }
        public string? Indicaciones { get; set; }

        public TratamientoEntity? Tratamiento { get; set; }

        private TratamientoPosologiaEntity() { }

        public TratamientoPosologiaEntity(Guid tratamientoId, string? dosis, string? frecuencia, int? duracionDias, string? indicaciones)
        {
            Id = Guid.NewGuid();
            TratamientoId = tratamientoId;
            Dosis = dosis;
            Frecuencia = frecuencia;
            DuracionDias = duracionDias;
            Indicaciones = indicaciones;
        }

        public void Update(Guid tratamientoId, string? dosis, string? frecuencia, int? duracionDias, string? indicaciones)
        {
            TratamientoId = tratamientoId;
            Dosis = dosis;
            Frecuencia = frecuencia;
            DuracionDias = duracionDias;
            Indicaciones = indicaciones;
        }
    }
}
