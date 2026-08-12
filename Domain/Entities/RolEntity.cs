using Domain.ValueObjects;

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
            NombreRol = RolValueObject.Create(nombreRol).Value;
            Descripcion = descripcion;
            Activo = activo;
        }

        public void Update(string nombreRol, string? descripcion, bool activo)
        {
            NombreRol = RolValueObject.Create(nombreRol).Value;
            Descripcion = descripcion;
            Activo = activo;
        }
    }
}
