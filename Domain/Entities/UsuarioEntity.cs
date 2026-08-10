namespace Domain.Entities
{
    public class UsuarioEntity
    {
        public int IdUsuario { get; set; }
        public int? IdRol { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public string? JwtCode { get; set; }

        public RolEntity? Rol { get; set; }
        public ICollection<EmpleadoEntity> Empleados { get; set; } = [];
    }
}

