namespace Domain.Entities
{
    public class DiagnosticoEntity
    {
        public Guid Id { get; set; }
        public string CodigoCie10 { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Activo { get; set; }

        private DiagnosticoEntity() { }

        public DiagnosticoEntity(string codigoCie10, string descripcion, bool activo)
        {
            Id = Guid.NewGuid();
            CodigoCie10 = codigoCie10;
            Descripcion = descripcion;
            Activo = activo;
        }
    }
}
