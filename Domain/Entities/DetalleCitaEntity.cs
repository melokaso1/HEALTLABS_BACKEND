namespace Domain.Entities
{
    public class DetalleCitaEntity
    {
        public Guid Id { get; private set; }
        public Guid CitaId { get; private set; }
        public int ProfesionalId { get; private set; }
        public string? NotaAtencion { get; private set; }
        public string? ResumenConsulta { get; private set; }
        public DateTime FechaRegistro { get; private set; }

        public CitaEntity? Cita { get; private set; }
        public ICollection<DetalleDiagnosticoEntity> DetallesDiagnostico { get; private set; } = [];
        public ICollection<AtencionTratamientoEntity> AtencionesTratamiento { get; private set; } = [];

        private DetalleCitaEntity() { }

        public DetalleCitaEntity(Guid citaId, int profesionalId, string? notaAtencion,
            string? resumenConsulta, DateTime fechaRegistro)
        {
            Id = Guid.NewGuid();
            CitaId = citaId;
            ProfesionalId = profesionalId;
            NotaAtencion = notaAtencion;
            ResumenConsulta = resumenConsulta;
            FechaRegistro = fechaRegistro;
        }

        public void Update(Guid citaId, int profesionalId, string? notaAtencion,
            string? resumenConsulta, DateTime fechaRegistro)
        {
            CitaId = citaId;
            ProfesionalId = profesionalId;
            NotaAtencion = notaAtencion;
            ResumenConsulta = resumenConsulta;
            FechaRegistro = fechaRegistro;
        }
    }
}
