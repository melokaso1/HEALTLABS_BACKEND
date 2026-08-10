namespace Domain.Entities
{
    public class CitaEntity
    {
        public Guid Id { get; set; }
        public Guid PacienteId { get; set; }
        public Guid MedicoId { get; set; }
        public Guid EstadoCitaId { get; set; }
        public Guid TipoCitaId { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public string MotivoConsulta { get; set; } = null!;
        public string? Observaciones { get; set; }
        public Guid UsuarioCreacionId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? MotivoCancelacion { get; set; }
        public Guid? UsuarioCancelacionId { get; set; }
        public DateTime? FechaCancelacion { get; set; }

        public PacienteEntity? Paciente { get; set; }
        public MedicoEntity? Medico { get; set; }
        public EstadoCitaEntity? EstadoCita { get; set; }
        public TipoCitaEntity? TipoCita { get; set; }
        public UsuarioEntity? UsuarioCreacion { get; set; }
        public UsuarioEntity? UsuarioCancelacion { get; set; }
        public ICollection<CitaHistorialEstadoEntity> HistorialEstados { get; set; } = [];
        public DetalleCitaEntity? DetalleCita { get; set; }

        private CitaEntity() { }

        public CitaEntity(
            Guid pacienteId,
            Guid medicoId,
            Guid estadoCitaId,
            Guid tipoCitaId,
            DateOnly fecha,
            TimeOnly horaInicio,
            TimeOnly horaFin,
            string motivoConsulta,
            string? observaciones,
            Guid usuarioCreacionId)
        {
            Id = Guid.NewGuid();
            PacienteId = pacienteId;
            MedicoId = medicoId;
            EstadoCitaId = estadoCitaId;
            TipoCitaId = tipoCitaId;
            Fecha = fecha;
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            MotivoConsulta = motivoConsulta;
            Observaciones = observaciones;
            UsuarioCreacionId = usuarioCreacionId;
            FechaCreacion = DateTime.UtcNow;
        }

        public void Update(
            Guid pacienteId,
            Guid medicoId,
            Guid estadoCitaId,
            Guid tipoCitaId,
            DateOnly fecha,
            TimeOnly horaInicio,
            TimeOnly horaFin,
            string motivoConsulta,
            string? observaciones,
            Guid usuarioCreacionId,
            string? motivoCancelacion,
            Guid? usuarioCancelacionId,
            DateTime? fechaCancelacion)
        {
            PacienteId = pacienteId;
            MedicoId = medicoId;
            EstadoCitaId = estadoCitaId;
            TipoCitaId = tipoCitaId;
            Fecha = fecha;
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            MotivoConsulta = motivoConsulta;
            Observaciones = observaciones;
            UsuarioCreacionId = usuarioCreacionId;
            MotivoCancelacion = motivoCancelacion;
            UsuarioCancelacionId = usuarioCancelacionId;
            FechaCancelacion = fechaCancelacion;
        }
    }
}
