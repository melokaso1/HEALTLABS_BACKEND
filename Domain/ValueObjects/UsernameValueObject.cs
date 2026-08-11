using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public sealed record UsernameValueObject
{
    public const int LongitudMinima = 5;
    public const int LongitudMaxima = 100;

    private static readonly Regex PatronUsername = new(
        @"^[a-z0-9._\-]+$",
        RegexOptions.CultureInvariant,
        TimeSpan.FromSeconds(1));

    public string Value { get; }

    private UsernameValueObject(string value)
    {
        Value = value;
    }

    public static UsernameValueObject Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre de usuario es obligatorio.", nameof(value));

        var usernameNormalizado = value.Trim().ToLowerInvariant();

        if (usernameNormalizado.Length < LongitudMinima || usernameNormalizado.Length > LongitudMaxima)
            throw new ArgumentException($"El nombre de usuario debe tener entre {LongitudMinima} y {LongitudMaxima} caracteres.", nameof(value));

        if (!PatronUsername.IsMatch(usernameNormalizado))
            throw new ArgumentException("El nombre de usuario solo puede contener letras, numeros, puntos, guiones y guiones bajos.", nameof(value));

        return new UsernameValueObject(usernameNormalizado);
    }

    public override string ToString() => Value;
}
