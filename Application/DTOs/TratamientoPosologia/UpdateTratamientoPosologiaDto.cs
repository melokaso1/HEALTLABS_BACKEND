namespace Application.DTOs.TratamientoPosologia
{
    public class UpdateTratamientoPosologiaDto
    {
        public Guid TratamientoId { get; set; }
        public string? Dosis { get; set; }
        public string? Frecuencia { get; set; }
        public int? DuracionDias { get; set; }
        public string? Indicaciones { get; set; }
    }
}
