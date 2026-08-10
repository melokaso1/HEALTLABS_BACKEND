namespace Domain.Entities
{
    public class DetalleCita
    {
        public Guid IdDetalle { get; set; }
        public int CitaId { get; set; }
        public int ProfesionalId { get; set; }
        public string? NotaAtencion { get; set; }
        public string? ResumenConsulta { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Cita? Cita { get; set; }
        public ICollection<DetalleDiagnostico> DetallesDiagnostico { get; set; } = [];
        public ICollection<AtencionTratamiento> AtencionesTratamiento { get; set; } = [];
    }
}
