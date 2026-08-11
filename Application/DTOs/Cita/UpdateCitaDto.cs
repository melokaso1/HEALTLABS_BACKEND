namespace Application.DTOs.Cita
{
    public class UpdateCitaDto
    {
        public Guid PacienteId { get; set; }
        public Guid MedicoId { get; set; }
        public Guid EstadoCitaId { get; set; }
        public Guid TipoCitaId { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public string MotivoConsulta { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        public string? MotivoCancelacion { get; set; }
        public Guid? UsuarioCancelacionId { get; set; }
        public DateTime? FechaCancelacion { get; set; }
    }
}
