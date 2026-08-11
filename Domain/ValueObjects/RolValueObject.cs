namespace Domain.ValueObjects;

public sealed record RolValueObject
{
    public const string Administrador = "Administrador";
    public const string Profesional = "Profesional";
    public const string Recepcionista = "Recepcionista";

    private static readonly HashSet<string> RolesPermitidos =
    [
        Administrador,
        Profesional,
        Recepcionista
    ];

    public static IReadOnlyCollection<string> ValoresPermitidos => RolesPermitidos;

    public string Value { get; }

    private RolValueObject(string value)
    {
        Value = value;
    }

    public static RolValueObject Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El rol es obligatorio.", nameof(value));

        var rolNormalizado = value.Trim();

        if (!RolesPermitidos.Contains(rolNormalizado))
            throw new ArgumentException(
                $"El rol '{rolNormalizado}' no es valido. Roles permitidos: {string.Join(", ", RolesPermitidos)}.",
                nameof(value));

        return new RolValueObject(rolNormalizado);
    }

    public static RolValueObject RolAdministrador() => new(Administrador);

    public static RolValueObject RolProfesional() => new(Profesional);

    public static RolValueObject RolRecepcionista() => new(Recepcionista);

    public override string ToString() => Value;
}
