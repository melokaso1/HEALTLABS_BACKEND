namespace Domain.Entities
{
    public class AtencionTratamiento
    {
        public Guid IdAtencionTratamiento { get; set; }
        public int DetalleCitaId { get; set; }
        public int TratamientoId { get; set; }
        public string? DosisPersonalizada { get; set; }
        public string? FrecuenciaPersonalizada { get; set; }
        public int? DuracionDiasPersonalizada { get; set; }
        public string? IndicacionesPersonalizadas { get; set; }

        public DetalleCita? DetalleCita { get; set; }
        public Tratamiento? Tratamiento { get; set; }
    }
}
