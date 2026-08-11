namespace Application.DTOs.TokenRecuperacion;

public class UpdateTokenRecuperacionDto
{
    public Guid UsuarioId { get; set; }
    public string TokenHash { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public DateTime? UsadoEn { get; set; }
    public string? IpSolicitud { get; set; }
}
