namespace Domain.Entities
{
    public class DetalleDiagnosticoEntity
    {
        public Guid Id { get; set; }
        public Guid DetalleCitaId { get; set; }
        public Guid DiagnosticoId { get; set; }
        public bool Principal { get; set; }

        public DetalleCitaEntity? DetalleCita { get; set; }
        public DiagnosticoEntity? Diagnostico { get; set; }

        private DetalleDiagnosticoEntity() { }

        public DetalleDiagnosticoEntity(Guid detalleCitaId, Guid diagnosticoId, bool principal)
        {
            Id = Guid.NewGuid();
            DetalleCitaId = detalleCitaId;
            DiagnosticoId = diagnosticoId;
            Principal = principal;
        }
    }
}
