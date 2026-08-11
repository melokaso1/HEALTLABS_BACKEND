using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public sealed record NumeroDocumentoValueObject
{
    public const int LongitudMaxima = 50;

    private static readonly Regex PatronDocumento = new(
        @"^[A-Z0-9]+$",
        RegexOptions.CultureInvariant,
        TimeSpan.FromSeconds(1));

    public string Value { get; }

    private NumeroDocumentoValueObject(string value)
    {
        Value = value;
    }

    public static NumeroDocumentoValueObject Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El numero de documento es obligatorio.", nameof(value));

        var numeroNormalizado = value
            .Trim()
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty)
            .ToUpperInvariant();

        if (numeroNormalizado.Length > LongitudMaxima)
            throw new ArgumentException($"El numero de documento no puede superar {LongitudMaxima} caracteres.", nameof(value));

        if (!PatronDocumento.IsMatch(numeroNormalizado))
            throw new ArgumentException("El numero de documento solo puede contener letras y numeros.", nameof(value));

        return new NumeroDocumentoValueObject(numeroNormalizado);
    }

    public override string ToString() => Value;
}
