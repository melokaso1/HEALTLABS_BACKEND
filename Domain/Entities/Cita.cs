namespace Domain.Entities
{
    public class Cita
    {
        public int IdCita { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public int IdEstadoCita { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public string MotivoConsulta { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public EstadoCita? EstadoCita { get; set; }
        public ICollection<DetalleCita> DetallesCita { get; set; } = [];
    }
}
