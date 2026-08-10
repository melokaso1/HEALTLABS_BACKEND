namespace Domain.Entities
{
    public class DetalleCitaEntity
    {
        public Guid IdDetalle { get; set; }
        public int CitaId { get; set; }
        public int ProfesionalId { get; set; }
        public string? NotaAtencion { get; set; }
        public string? ResumenConsulta { get; set; }
        public DateTime FechaRegistro { get; set; }

        public CitaEntity? Cita { get; set; }
        public ICollection<DetalleDiagnosticoEntity> DetallesDiagnostico { get; set; } = [];
        public ICollection<AtencionTratamientoEntity> AtencionesTratamiento { get; set; } = [];
    }
}
