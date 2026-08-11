using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public sealed record EmailValueObject
{
    public const int LongitudMaxima = 150;

    private static readonly Regex PatronEmail = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.CultureInvariant,
        TimeSpan.FromSeconds(1));

    public string Value { get; }

    private EmailValueObject(string value)
    {
        Value = value;
    }

    public static EmailValueObject Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El correo electronico es obligatorio.", nameof(value));

        var emailNormalizado = value.Trim().ToLowerInvariant();

        if (emailNormalizado.Length > LongitudMaxima)
            throw new ArgumentException($"El correo electronico no puede superar {LongitudMaxima} caracteres.", nameof(value));

        if (!PatronEmail.IsMatch(emailNormalizado))
            throw new ArgumentException("El correo electronico no tiene un formato valido.", nameof(value));

        return new EmailValueObject(emailNormalizado);
    }

    public override string ToString() => Value;
}
