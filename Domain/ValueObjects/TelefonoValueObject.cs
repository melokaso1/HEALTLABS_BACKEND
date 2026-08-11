using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public sealed record TelefonoValueObject
{
    public const int LongitudMinima = 7;
    public const int LongitudMaxima = 30;

    private static readonly Regex PatronTelefono = new(
        @"^\+?[0-9]{7,30}$",
        RegexOptions.CultureInvariant,
        TimeSpan.FromSeconds(1));

    public string Value { get; }

    private TelefonoValueObject(string value)
    {
        Value = value;
    }

    public static TelefonoValueObject Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El telefono es obligatorio.", nameof(value));

        var telefonoNormalizado = value
            .Trim()
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty)
            .Replace("(", string.Empty)
            .Replace(")", string.Empty)
            .Replace(".", string.Empty);

        if (!PatronTelefono.IsMatch(telefonoNormalizado))
            throw new ArgumentException(
                $"El telefono debe contener entre {LongitudMinima} y {LongitudMaxima} digitos, con un '+' opcional al inicio.",
                nameof(value));

        return new TelefonoValueObject(telefonoNormalizado);
    }

    public override string ToString() => Value;
}
