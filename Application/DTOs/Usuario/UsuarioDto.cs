namespace Application.DTOs.Usuario
{
    public class UsuarioDto
    {
        public Guid UsuarioId { get; set; }
        public Guid EmpleadoId { get; set; }
        public Guid? MedicoId { get; set; }
        public Guid RolId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? UltimoLogin { get; set; }
        public bool DebeCambiarPassword { get; set; }
        public int TokenVersion { get; set; }
        public string? NombreCompleto { get; set; }
        public string? RolNombre { get; set; }
    }
}
