using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public sealed record PasswordValueObject
{
    public const int LongitudMinima = 8;
    public const int LongitudMaxima = 128;

    private static readonly Regex PatronPassword = new(
        @"^[^\s]+$",
        RegexOptions.CultureInvariant,
        TimeSpan.FromSeconds(1));

    public string Value { get; }

    private PasswordValueObject(string value)
    {
        Value = value;
    }

    public static PasswordValueObject Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("La contraseña es obligatoria.", nameof(value));

        if (value.Length < LongitudMinima || value.Length > LongitudMaxima)
            throw new ArgumentException($"La contraseña debe tener entre {LongitudMinima} y {LongitudMaxima} caracteres.", nameof(value));

        if (!PatronPassword.IsMatch(value))
            throw new ArgumentException("La contraseña no puede contener espacios.", nameof(value));

        return new PasswordValueObject(value);
    }

    public override string ToString() => Value;
}
