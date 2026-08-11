using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public sealed record DescripcionValueObject
{
    public const int LongitudMaxima = 1000;

    private static readonly Regex PatronDescripcion = new(
        @"^[^\r\n\t]+$",
        RegexOptions.CultureInvariant,
        TimeSpan.FromSeconds(1));

    public string Value { get; }

    private DescripcionValueObject(string value)
    {
        Value = value;
    }

    public static DescripcionValueObject Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("La descripción es obligatoria.", nameof(value));

        var descripcionNormalizada = value.Trim();

        if (descripcionNormalizada.Length > LongitudMaxima)
            throw new ArgumentException($"La descripción no puede superar {LongitudMaxima} caracteres.", nameof(value));

        if (!PatronDescripcion.IsMatch(descripcionNormalizada))
            throw new ArgumentException("La descripción contiene caracteres inválidos.", nameof(value));

        return new DescripcionValueObject(descripcionNormalizada);
    }

    public override string ToString() => Value;
}
