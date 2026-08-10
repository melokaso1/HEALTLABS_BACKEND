namespace Domain.Entities
{
    public class DiagnosticoEntity
    {
        public Guid IdDiagnostico { get; set; }
        public string CodigoCie10 { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public ICollection<DetalleDiagnosticoEntity> DetallesDiagnostico { get; set; } = [];
    }
}
