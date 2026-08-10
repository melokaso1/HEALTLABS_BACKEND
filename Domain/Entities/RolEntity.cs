namespace Domain.Entities
{
    public class RolEntity
    {
        public Guid Id { get; set; }
        public string NombreRol { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }

        public ICollection<RolPermisoEntity> RolPermisos { get; set; } = [];

        private RolEntity() { }

        public RolEntity(string nombreRol, string? descripcion, bool activo = true)
        {
            Id = Guid.NewGuid();
            NombreRol = nombreRol;
            Descripcion = descripcion;
            Activo = activo;
        }

        public void Update(string nombreRol, string? descripcion, bool activo)
        {
            NombreRol = nombreRol;
            Descripcion = descripcion;
            Activo = activo;
        }
    }
}
