namespace Application.DTOs.TokenRecuperacion;

public class CreateTokenRecuperacionDto
{
    public Guid UsuarioId { get; set; }
    public string TokenHash { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public string? IpSolicitud { get; set; }
}
