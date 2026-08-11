using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public sealed record NombreValueObject
{
    public const int LongitudMinima = 2;
    public const int LongitudMaxima = 100;

    private static readonly Regex PatronNombre = new(
        @"^[\p{L}\p{M}0-9\.\-\'\s]+$",
        RegexOptions.CultureInvariant,
        TimeSpan.FromSeconds(1));

    public string Value { get; }

    private NombreValueObject(string value)
    {
        Value = value;
    }

    public static NombreValueObject Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre es obligatorio.", nameof(value));

        var nombreNormalizado = Regex.Replace(value.Trim(), "\s+", " ");

        if (nombreNormalizado.Length < LongitudMinima || nombreNormalizado.Length > LongitudMaxima)
            throw new ArgumentException($"El nombre debe tener entre {LongitudMinima} y {LongitudMaxima} caracteres.", nameof(value));

        if (!PatronNombre.IsMatch(nombreNormalizado))
            throw new ArgumentException("El nombre contiene caracteres inválidos.", nameof(value));

        return new NombreValueObject(nombreNormalizado);
    }

    public override string ToString() => Value;
}
