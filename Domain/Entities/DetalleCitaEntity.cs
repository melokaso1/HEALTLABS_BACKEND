namespace Domain.Entities
{
    public class DetalleCitaEntity
    {
        public Guid Id { get; set; }
        public Guid CitaId { get; set; }
        public Guid MedicoId { get; set; }
        public string? NotaAtencion { get; set; }
        public string? ResumenConsulta { get; set; }
        public DateTime FechaRegistro { get; set; }

        public CitaEntity? Cita { get; set; }
        public MedicoEntity? Medico { get; set; }
        public ICollection<DetalleDiagnosticoEntity> DetallesDiagnostico { get; set; } = [];
        public ICollection<AtencionTratamientoEntity> AtencionesTratamiento { get; set; } = [];

        private DetalleCitaEntity() { }

        public DetalleCitaEntity(
            Guid citaId,
            Guid medicoId,
            string? notaAtencion,
            string? resumenConsulta)
        {
            Id = Guid.NewGuid();
            CitaId = citaId;
            MedicoId = medicoId;
            NotaAtencion = notaAtencion;
            ResumenConsulta = resumenConsulta;
            FechaRegistro = DateTime.UtcNow;
        }
    }
}
