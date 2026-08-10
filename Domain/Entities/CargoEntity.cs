namespace Domain.Entities
{
    public class CargoEntity
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public int? NivelJerarquico { get; set; }

        private CargoEntity() { }

        public CargoEntity(string nombre, string? codigo, string? descripcion, int? nivelJerarquico)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Codigo = codigo;
            Descripcion = descripcion;
            NivelJerarquico = nivelJerarquico;
        }

        public void update(string nombre, string? codigo, string? descripcion, int? nivelJerarquico)
        {
            Nombre = nombre;
            Codigo = codigo;
            Descripcion = descripcion;
            NivelJerarquico = nivelJerarquico;
        }
    }
}
