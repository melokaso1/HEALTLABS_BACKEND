namespace Domain.Entities
{
    public class PermisoEntity
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Modulo { get; set; } = null!;
        public string? Descripcion { get; set; }

        public ICollection<RolPermisoEntity> RolPermisos { get; set; } = [];

        private PermisoEntity() { }

        public PermisoEntity(string codigo, string modulo, string? descripcion)
        {
            Id = Guid.NewGuid();
            Codigo = codigo;
            Modulo = modulo;
            Descripcion = descripcion;
        }

        public void Update(string codigo, string modulo, string? descripcion)
        {
            Codigo = codigo;
            Modulo = modulo;
            Descripcion = descripcion;
        }
    }
}
