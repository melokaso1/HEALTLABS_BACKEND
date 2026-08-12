namespace Application.DTOs.DetalleCita
{
    public class UpdateDetalleCitaDto
    {
        public Guid CitaId { get; set; }
        public Guid MedicoId { get; set; }
        public string? NotaAtencion { get; set; }
        public string? ResumenConsulta { get; set; }
    }
}