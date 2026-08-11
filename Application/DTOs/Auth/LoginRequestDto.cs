namespace Application.DTOs.Auth;

public class LoginRequestDto
{
    public string UsernameOrEmail { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
}
