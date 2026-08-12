namespace Domain.Entities
{
    public class UsuarioEntity
    {
        public Guid Id { get; set; }
        public Guid EmpleadoId { get; set; }
        public Guid RolId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? UltimoLogin { get; set; }
        public int IntentosFallidos { get; set; }
        public DateTime? BloqueadoHasta { get; set; }
        public bool DebeCambiarPassword { get; set; }
        public DateTime? PasswordChangedAt { get; set; }
        public int TokenVersion { get; set; }

        public EmpleadoEntity? Empleado { get; set; }
        public RolEntity? Rol { get; set; }
        public ICollection<SesionEntity> Sesiones { get; set; } = [];

        private UsuarioEntity() { }

        public UsuarioEntity(
            Guid empleadoId,
            Guid rolId,
            string username,
            string email,
            string passwordHash,
            bool activo,
            DateTime? ultimoLogin,
            int intentosFallidos,
            DateTime? bloqueadoHasta,
            bool debeCambiarPassword,
            DateTime? passwordChangedAt,
            int tokenVersion)
        {
            Id = Guid.NewGuid();
            EmpleadoId = empleadoId;
            RolId = rolId;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Activo = activo;
            FechaCreacion = DateTime.UtcNow;
            UltimoLogin = ultimoLogin;
            IntentosFallidos = intentosFallidos;
            BloqueadoHasta = bloqueadoHasta;
            DebeCambiarPassword = debeCambiarPassword;
            PasswordChangedAt = passwordChangedAt;
            TokenVersion = tokenVersion;
        }

        public void Update(
            Guid rolId,
            string username,
            string email,
            string passwordHash,
            bool activo,
            DateTime? ultimoLogin,
            int intentosFallidos,
            DateTime? bloqueadoHasta,
            bool debeCambiarPassword,
            DateTime? passwordChangedAt,
            int tokenVersion)
        {
            RolId = rolId;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Activo = activo;
            UltimoLogin = ultimoLogin;
            IntentosFallidos = intentosFallidos;
            BloqueadoHasta = bloqueadoHasta;
            DebeCambiarPassword = debeCambiarPassword;
            PasswordChangedAt = passwordChangedAt;
            TokenVersion = tokenVersion;
        }
    }
}
