namespace Domain.Entities
{
    public class DetalleDiagnostico
    {
        public int IdDetalleDiagnostico { get; set; }
        public int IdDetalleCita { get; set; }
        public int IdDiagnostico { get; set; }
        public bool Principal { get; set; }

        public DetalleCita? DetalleCita { get; set; }
        public Diagnostico? Diagnostico { get; set; }
    }
}
