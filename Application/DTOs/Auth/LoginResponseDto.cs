namespace Application.DTOs.Auth;

public class LoginResponseDto
{
    public Guid UsuarioId { get; set; }
    public Guid SesionId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string RolNombre { get; set; } = null!;
    public bool DebeCambiarPassword { get; set; }
    public DateTime SesionExpiresAt { get; set; }
    public string SessionToken { get; set; } = null!;
}
