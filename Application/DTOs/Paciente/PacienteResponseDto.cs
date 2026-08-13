using Domain.Entities;

namespace Application.DTOs.Paciente;

public sealed class PacienteResponseDto
{
    public Guid Id { get; init; }
    public bool Activo { get; init; }
    public string? TipoDocumento { get; init; }
    public string NumeroDocumento { get; init; } = string.Empty;
    public string NombreCompleto { get; init; } = string.Empty;
    public string? Sexo { get; init; }
    public DateOnly? FechaNacimiento { get; init; }
    public int? Edad { get; init; }
    public string? Telefono { get; init; }

    public static PacienteResponseDto FromEntity(PacienteEntity paciente)
    {
        var persona = paciente.Persona
            ?? throw new InvalidOperationException("El paciente no tiene una persona asociada.");

        return new PacienteResponseDto
        {
            Id = paciente.Id,
            Activo = paciente.Activo,
            TipoDocumento = persona.TipoDocumento?.Nombre,
            NumeroDocumento = persona.NumeroDocumento,
            NombreCompleto = $"{persona.Nombre} {persona.Apellido}".Trim(),
            Sexo = persona.Sexo?.Nombre,
            FechaNacimiento = persona.FechaNacimiento,
            Edad = CalculateAge(persona.FechaNacimiento),
            Telefono = persona.Telefonos
                .OrderByDescending(telefono => telefono.Principal)
                .Select(telefono => telefono.Telefono)
                .FirstOrDefault()
        };
    }

    private static int? CalculateAge(DateOnly? fechaNacimiento)
    {
        if (!fechaNacimiento.HasValue)
            return null;

        var hoy = DateOnly.FromDateTime(DateTime.UtcNow);
        var edad = hoy.Year - fechaNacimiento.Value.Year;
        if (fechaNacimiento.Value > hoy.AddYears(-edad))
            edad--;

        return edad;
    }
}
