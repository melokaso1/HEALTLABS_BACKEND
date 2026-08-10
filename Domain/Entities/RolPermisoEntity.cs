namespace Domain.Entities
{
    public class RolPermisoEntity
    {
        public Guid Id { get; set; }
        public Guid RolId { get; set; }
        public Guid PermisoId { get; set; }

        public RolEntity? Rol { get; set; }
        public PermisoEntity? Permiso { get; set; }

        private RolPermisoEntity() { }

        public RolPermisoEntity(Guid rolId, Guid permisoId)
        {
            Id = Guid.NewGuid();
            RolId = rolId;
            PermisoId = permisoId;
        }

        public void Update(Guid rolId, Guid permisoId)
        {
            RolId = rolId;
            PermisoId = permisoId;
        }
    }
}
