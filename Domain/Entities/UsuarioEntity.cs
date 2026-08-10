namespace Domain.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int? IdRol { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public string? JwtCode { get; set; }

        public RolEntity? Rol { get; set; }
        public ICollection<Empleado> Empleados { get; set; } = [];
    }
}
