namespace Domain.Entities
{
    public class RolEntity
    {
        public Guid Id { get; set; }
        public string NombreRol { get; set; } = null!;
        public string? Descripcion { get; set; }

        private RolEntity() { }

        public RolEntity(string nombreRol, string? descripcion)
        {
            Id = Guid.NewGuid();
            NombreRol = nombreRol;
            Descripcion = descripcion;
        }

        public void update(string nombreRol, string? descripcion)
        {
            NombreRol = nombreRol;
            Descripcion = descripcion;
        }
    }
}
