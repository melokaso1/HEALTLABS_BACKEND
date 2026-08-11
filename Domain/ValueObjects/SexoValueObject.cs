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

    public string Nombre => SexosPermitidos[Codigo];

    private SexoValueObject(string codigo)
    {
        Codigo = codigo;
    }

    public static SexoValueObject Create(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("El sexo es obligatorio.", nameof(codigo));

        var codigoNormalizado = codigo.Trim().ToUpperInvariant();

        if (!SexosPermitidos.ContainsKey(codigoNormalizado))
            throw new ArgumentException(
                $"El sexo '{codigo}' no es valido. Codigos permitidos: {string.Join(", ", SexosPermitidos.Keys)}.",
                nameof(codigo));

        return new SexoValueObject(codigoNormalizado);
    }

    public static SexoValueObject M() => new(Masculino);

    public static SexoValueObject F() => new(Femenino);

    public static SexoValueObject O() => new(Otro);

    public override string ToString() => Codigo;
}
