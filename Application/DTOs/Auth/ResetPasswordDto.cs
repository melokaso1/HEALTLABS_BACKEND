namespace Application.DTOs.Auth;

public class ResetPasswordDto
{
    public Guid TokenId { get; set; }
    public string TokenRaw { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
