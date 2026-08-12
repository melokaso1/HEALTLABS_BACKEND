namespace Domain.Entities
{
    public class CargoEntity
    {
        public Guid Id { get; set; }
        public string? Codigo { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int? NivelJerarquico { get; set; }

        private CargoEntity() { }

        public CargoEntity(string? codigo, string nombre, string? descripcion, int? nivelJerarquico)
        {
            Id = Guid.NewGuid();
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            NivelJerarquico = nivelJerarquico;
        }

        public void Update(string? codigo, string nombre, string? descripcion, int? nivelJerarquico)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            NivelJerarquico = nivelJerarquico;
        }
    }
}
