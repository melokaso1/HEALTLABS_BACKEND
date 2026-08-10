namespace Domain.Entities
{
    public class DetalleDiagnosticoEntity
    {
        public Guid IdDetalleDiagnostico { get; set; }
        public int DetalleCitaId { get; set; }
        public int DiagnosticoId { get; set; }
        public bool Principal { get; set; }

        public DetalleCitaEntity? DetalleCita { get; set; }
        public DiagnosticoEntity? Diagnostico { get; set; }
    }
}
