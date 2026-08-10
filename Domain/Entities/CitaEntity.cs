namespace Domain.Entities
{
    public class CitaEntity
    {
        public Guid Id { get; set; }
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

        private CitaEntity() { }

        public CitaEntity(
                         int pacienteId,
                         int profesionalId,
                         int estadoCitaId,
                         DateOnly fecha,
                         TimeOnly horaInicio,
                         TimeOnly horaFin,
                         string motivoConsulta,
                         string observaciones,
                         int idUsuarioCreacion,
                         DateTime fechaCreacion
           )
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
    }
}
