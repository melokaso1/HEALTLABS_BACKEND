using System.Text;

namespace Infrastructure.Auth;

public sealed class JwtSettings
{
    public JwtSettings(string? key, string? issuer, string? audience, int durationInMinutes)
    {
        Key = key ?? string.Empty;
        Issuer = issuer ?? string.Empty;
        Audience = audience ?? string.Empty;
        DurationInMinutes = durationInMinutes;
    }

    public string Key { get; }
    public string Issuer { get; }
    public string Audience { get; }
    public int DurationInMinutes { get; }

    public void Validate()
    {
        if (Encoding.UTF8.GetByteCount(Key) < 32)
            throw new InvalidOperationException("JWT:Key debe tener al menos 32 bytes.");

        if (string.IsNullOrWhiteSpace(Issuer) || string.IsNullOrWhiteSpace(Audience))
            throw new InvalidOperationException("JWT:Issuer y JWT:Audience son obligatorios.");

        if (DurationInMinutes is <= 0 or > 60)
            throw new InvalidOperationException("JWT:DurationInMinutes debe estar entre 1 y 60.");
    }
}
