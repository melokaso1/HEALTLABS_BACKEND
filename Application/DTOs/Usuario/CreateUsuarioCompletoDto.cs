using Application.DTOs.Persona;

namespace Application.DTOs.Usuario;

public sealed class CreateUsuarioCompletoDto
{
    public CreatePersonaDto Persona { get; set; } = new();
    public Guid RolId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
    public bool DebeCambiarPassword { get; set; }
    public DateOnly FechaIngreso { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public string? Telefono { get; set; }
    public string? TipoTelefono { get; set; }
    public string? Direccion { get; set; }
    public string? Ciudad { get; set; }
    public string? RegistroProfesional { get; set; }
    public Guid? EspecialidadId { get; set; }
    public bool EspecialidadPrincipal { get; set; } = true;
}
