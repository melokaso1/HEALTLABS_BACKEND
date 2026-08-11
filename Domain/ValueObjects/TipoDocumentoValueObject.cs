namespace Domain.ValueObjects;

public sealed record TipoDocumentoValueObject
{
    public const string CedulaCiudadania = "CC";
    public const string TarjetaIdentidad = "TI";
    public const string CedulaExtranjeria = "CE";
    public const string Pasaporte = "PA";
    public const string RegistroCivil = "RC";

    private static readonly IReadOnlyDictionary<string, string> TiposPermitidos =
        new Dictionary<string, string>
        {
            [CedulaCiudadania] = "Cedula de ciudadania",
            [TarjetaIdentidad] = "Tarjeta de identidad",
            [CedulaExtranjeria] = "Cedula de extranjeria",
            [Pasaporte] = "Pasaporte",
            [RegistroCivil] = "Registro civil"
        };

    public static IReadOnlyCollection<string> CodigosPermitidos => TiposPermitidos.Keys.ToArray();

    public string Codigo { get; }

    public string Nombre => TiposPermitidos[Codigo];

    private TipoDocumentoValueObject(string codigo)
    {
        Codigo = codigo;
    }

    public static TipoDocumentoValueObject Create(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("El tipo de documento es obligatorio.", nameof(codigo));

        var codigoNormalizado = codigo.Trim().ToUpperInvariant();

        if (!TiposPermitidos.ContainsKey(codigoNormalizado))
            throw new ArgumentException(
                $"El tipo de documento '{codigo}' no es valido. Codigos permitidos: {string.Join(", ", TiposPermitidos.Keys)}.",
                nameof(codigo));

        return new TipoDocumentoValueObject(codigoNormalizado);
    }

    public static TipoDocumentoValueObject CC() => new(CedulaCiudadania);

    public static TipoDocumentoValueObject TI() => new(TarjetaIdentidad);

    public static TipoDocumentoValueObject CE() => new(CedulaExtranjeria);

    public static TipoDocumentoValueObject PA() => new(Pasaporte);

    public static TipoDocumentoValueObject RC() => new(RegistroCivil);

    public override string ToString() => Codigo;
}
