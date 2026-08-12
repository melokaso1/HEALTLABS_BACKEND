using System.Text;

namespace Infrastructure.Auth;

public sealed class JwtSettings
{
    public JwtSettings(string? key, string? issuer, string? audience, int durationInMinutes, int refreshDurationInDays)
    {
        Key = key ?? string.Empty;
        Issuer = issuer ?? string.Empty;
        Audience = audience ?? string.Empty;
        DurationInMinutes = durationInMinutes;
        RefreshDurationInDays = refreshDurationInDays;
    }

    public string Key { get; }
    public string Issuer { get; }
    public string Audience { get; }
    public int DurationInMinutes { get; }
    public int RefreshDurationInDays { get; }

    public void Validate()
    {
        if (Encoding.UTF8.GetByteCount(Key) < 32)
            throw new InvalidOperationException("JWT:Key debe tener al menos 32 bytes.");

        if (string.IsNullOrWhiteSpace(Issuer) || string.IsNullOrWhiteSpace(Audience))
            throw new InvalidOperationException("JWT:Issuer y JWT:Audience son obligatorios.");

        if (DurationInMinutes is <= 0 or > 1440)
            throw new InvalidOperationException("JWT:DurationInMinutes debe estar entre 1 y 1440.");

        if (RefreshDurationInDays is <= 0 or > 90)
            throw new InvalidOperationException("JWT:RefreshDurationInDays debe estar entre 1 y 90.");
    }
}
