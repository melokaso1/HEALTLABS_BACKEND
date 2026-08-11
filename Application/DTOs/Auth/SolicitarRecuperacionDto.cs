namespace Application.DTOs.Auth;

public class SolicitarRecuperacionDto
{
    public string EmailOrUsername { get; set; } = null!;
    public string? IpAddress { get; set; }
}
