namespace Application.DTOs.Usuario
{
    public class UpdateUsuarioDto
    {
        public Guid EmpleadoId { get; set; }
        public Guid RolId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }
        public bool Activo { get; set; } = true;
        public bool DebeCambiarPassword { get; set; }
    }
}
