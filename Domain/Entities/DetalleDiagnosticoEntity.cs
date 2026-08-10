namespace Domain.Entities
{
    public class DetalleDiagnostico
    {
        public Guid IdDetalleDiagnostico { get; set; }
        public int DetalleCitaId { get; set; }
        public int DiagnosticoId { get; set; }
        public bool Principal { get; set; }

        public DetalleCita? DetalleCita { get; set; }
        public Diagnostico? Diagnostico { get; set; }
    }
}
