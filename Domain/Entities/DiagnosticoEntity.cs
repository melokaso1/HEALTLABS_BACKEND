namespace Domain.Entities
{
    public class DiagnosticoEntity
    {
        public Guid Id { get; private set; }
        public string CodigoCie10 { get; private set; } = null!;
        public string Descripcion { get; private set; } = null!;
        public bool Activo { get; private set; }

        public ICollection<DetalleDiagnosticoEntity> DetallesDiagnostico { get; private set; } = [];

        private DiagnosticoEntity() { }

        public DiagnosticoEntity(string codigoCie10, string descripcion, bool activo)
        {
            Id = Guid.NewGuid();
            CodigoCie10 = codigoCie10;
            Descripcion = descripcion;
            Activo = activo;
        }

        public void Update(string codigoCie10, string descripcion, bool activo)
        {
            CodigoCie10 = codigoCie10;
            Descripcion = descripcion;
            Activo = activo;
        }
    }
}
