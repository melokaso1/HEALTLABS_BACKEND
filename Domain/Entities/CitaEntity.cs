namespace Domain.Entities
{
    public class CitaEntity
    {
        public Guid Id { get; private set; }
        public int PacienteId { get; private set; }
        public int ProfesionalId { get; private set; }
        public Guid EstadoCitaId { get; private set; }
        public DateOnly Fecha { get; private set; }
        public TimeOnly HoraInicio { get; private set; }
        public TimeOnly HoraFin { get; private set; }
        public string MotivoConsulta { get; private set; } = null!;
        public string? Observaciones { get; private set; }
        public int IdUsuarioCreacion { get; private set; }
        public DateTime FechaCreacion { get; private set; }

        public EstadoCitaEntity? EstadoCita { get; private set; }
        public ICollection<DetalleCitaEntity> DetallesCita { get; private set; } = [];

        private CitaEntity() { }

        public CitaEntity(int pacienteId, int profesionalId, Guid estadoCitaId, DateOnly fecha,
            TimeOnly horaInicio, TimeOnly horaFin, string motivoConsulta, string? observaciones,
            int idUsuarioCreacion, DateTime fechaCreacion)
        {
            Id = Guid.NewGuid();
            PacienteId = pacienteId;
            ProfesionalId = profesionalId;
            EstadoCitaId = estadoCitaId;
            Fecha = fecha;
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            MotivoConsulta = motivoConsulta;
            Observaciones = observaciones;
            IdUsuarioCreacion = idUsuarioCreacion;
            FechaCreacion = fechaCreacion;
        }

        public void Update(int pacienteId, int profesionalId, Guid estadoCitaId, DateOnly fecha,
            TimeOnly horaInicio, TimeOnly horaFin, string motivoConsulta, string? observaciones)
        {
            PacienteId = pacienteId;
            ProfesionalId = profesionalId;
            EstadoCitaId = estadoCitaId;
            Fecha = fecha;
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            MotivoConsulta = motivoConsulta;
            Observaciones = observaciones;
        }
    }
}
