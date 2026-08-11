namespace Application.DTOs.DetalleDiagnostico
{
    public class UpdateDetalleDiagnosticoDto
    {
        public Guid DetalleCitaId { get; set; }
        public Guid DiagnosticoId { get; set; }
        public bool Principal { get; set; }
    }
}
