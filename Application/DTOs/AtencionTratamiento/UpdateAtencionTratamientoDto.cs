namespace Application.DTOs.AtencionTratamiento
{
    public class UpdateAtencionTratamientoDto
    {
        public Guid DetalleCitaId { get; set; }
        public Guid TratamientoId { get; set; }
        public string Dosis { get; set; } = string.Empty;
        public string? Frecuencia { get; set; }
        public int? DuracionDias { get; set; }
        public string? Indicaciones { get; set; }
    }
}
