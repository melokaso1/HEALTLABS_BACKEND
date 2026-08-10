namespace Domain.Entities
{
    public class DetalleDiagnosticoEntity
    {
        public Guid Id { get; private set; }
        public Guid DetalleCitaId { get; private set; }
        public Guid DiagnosticoId { get; private set; }
        public bool Principal { get; private set; }

        public DetalleCitaEntity? DetalleCita { get; private set; }
        public DiagnosticoEntity? Diagnostico { get; private set; }

        private DetalleDiagnosticoEntity() { }

        public DetalleDiagnosticoEntity(Guid detalleCitaId, Guid diagnosticoId, bool principal)
        {
            Id = Guid.NewGuid();
            DetalleCitaId = detalleCitaId;
            DiagnosticoId = diagnosticoId;
            Principal = principal;
        }

        public void Update(Guid detalleCitaId, Guid diagnosticoId, bool principal)
        {
            DetalleCitaId = detalleCitaId;
            DiagnosticoId = diagnosticoId;
            Principal = principal;
        }
    }
}
