using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IJwtTokenGenerator
    {
        int AccessDurationMinutes { get; }
        int RefreshDurationInDays { get; }
        string GenerateAccessToken(UsuarioEntity usuario, string rolNombre, string jti);
        string GenerateRefreshToken();
        string HashToken(string rawToken);
    }
}
