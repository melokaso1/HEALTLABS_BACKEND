namespace Application.DTOs.Auth;

public class LogoutRequestDto
{
    public string? Jti { get; set; }
    public Guid? SesionId { get; set; }
}
