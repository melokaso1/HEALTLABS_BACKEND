namespace Application.DTOs.Cita
{
    public class CreateCitaDto
    {
        public Guid PacienteId { get; set; }
        public Guid MedicoId { get; set; }
        public Guid TipoCitaId { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public string MotivoConsulta { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        public Guid UsuarioCreacionId { get; set; }
    }
}
