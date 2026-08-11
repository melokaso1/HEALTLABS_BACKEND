namespace Domain.ValueObjects;

public sealed record SexoValueObject
{
    public const string Masculino = "M";
    public const string Femenino = "F";
    public const string Otro = "O";

    private static readonly IReadOnlyDictionary<string, string> SexosPermitidos =
        new Dictionary<string, string>
        {
            [Masculino] = "Masculino",
            [Femenino] = "Femenino",
            [Otro] = "Otro"
        };

    public static IReadOnlyCollection<string> CodigosPermitidos => SexosPermitidos.Keys.ToArray();

    public string Codigo { get; }

    public string Nombre { get; }

    private SexoValueObject(string codigo, string nombre)
    {
        Codigo = codigo;
        Nombre = nombre;
    }

    public static SexoValueObject Create(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("El sexo es obligatorio.", nameof(codigo));

        var codigoNormalizado = codigo.Trim().ToUpperInvariant();

        if (!SexosPermitidos.TryGetValue(codigoNormalizado, out var nombre))
            throw new ArgumentException(
                $"El sexo '{codigoNormalizado}' no es valido. Codigos permitidos: {string.Join(", ", SexosPermitidos.Keys)}.",
                nameof(codigo));

        return new SexoValueObject(codigoNormalizado, nombre);
    }

    public static SexoValueObject M() => new(Masculino, SexosPermitidos[Masculino]);

    public static SexoValueObject F() => new(Femenino, SexosPermitidos[Femenino]);

    public static SexoValueObject O() => new(Otro, SexosPermitidos[Otro]);

    public override string ToString() => Codigo;
}
