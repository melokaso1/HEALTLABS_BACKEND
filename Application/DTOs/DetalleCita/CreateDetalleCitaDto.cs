namespace Application.DTOs.DetalleCita
{
    public class CreateDetalleCitaDto
    {
        public Guid CitaId { get; set; }
        public Guid MedicoId { get; set; }
        public string? NotaAtencion { get; set; }
        public string? ResumenConsulta { get; set; }
    }
}
