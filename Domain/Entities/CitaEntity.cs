namespace Domain.Entities
{
    public class CitaEntity
    {
        public Guid IdCita { get; set; }
        public int PacienteId { get; set; }
        public int ProfesionalId { get; set; }
        public int EstadoCitaId { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public string MotivoConsulta { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public EstadoCitaEntity? EstadoCita { get; set; }
        public ICollection<DetalleCitaEntity> DetallesCita { get; set; } = [];
    }
}
