namespace Application.DTOs.Auth;

public class RefreshTokenRequestDto
{
    public string RefreshToken { get; set; } = null!;
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
}
