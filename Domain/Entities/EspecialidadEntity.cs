namespace Domain.Entities
{
    public class EspecialidadEntity
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        private EspecialidadEntity() { }

        public EspecialidadEntity(string nombre, string? descripcion)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public void Update(string nombre, string? descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
        }
    }
}
