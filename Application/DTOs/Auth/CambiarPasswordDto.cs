namespace Application.DTOs.Auth
{
    public class CambiarPasswordDto
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
