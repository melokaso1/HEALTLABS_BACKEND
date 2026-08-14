using Domain.Entities;

namespace Application.DTOs.Paciente;

public sealed class PacienteResponseDto
{
    public Guid Id { get; init; }
    public bool Activo { get; init; }
    public string? TipoSangre { get; init; }
    public DateTime FechaRegistro { get; init; }
    public PersonaResumenDto Persona { get; init; } = null!;

    public static PacienteResponseDto FromEntity(PacienteEntity paciente)
    {
        var persona = paciente.Persona
            ?? throw new InvalidOperationException("El paciente no tiene una persona asociada.");

        return new PacienteResponseDto
        {
            Id = paciente.Id,
            Activo = paciente.Activo,
            TipoSangre = paciente.TipoSangre,
            FechaRegistro = paciente.FechaRegistro,
            Persona = PersonaResumenDto.FromEntity(persona)
        };
    }
}

public sealed class PersonaResumenDto
{
    public Guid Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string Apellido { get; init; } = string.Empty;
    public string NumeroDocumento { get; init; } = string.Empty;
    public Guid TipoDocumentoId { get; init; }
    public string? TipoDocumento { get; init; }
    public DateOnly? FechaNacimiento { get; init; }
    public int? Edad { get; init; }
    public Guid? SexoId { get; init; }
    public string? Sexo { get; init; }

    public static PersonaResumenDto FromEntity(PersonaEntity persona)
    {
        return new PersonaResumenDto
        {
            Id = persona.Id,
            Nombre = persona.Nombre,
            Apellido = persona.Apellido,
            NumeroDocumento = persona.NumeroDocumento,
            TipoDocumentoId = persona.TipoDocumentoId,
            TipoDocumento = persona.TipoDocumento?.Nombre,
            FechaNacimiento = persona.FechaNacimiento,
            Edad = CalcularEdad(persona.FechaNacimiento),
            SexoId = persona.SexoId,
            Sexo = persona.Sexo?.Nombre
        };
    }

    private static int? CalcularEdad(DateOnly? fechaNacimiento)
    {
        if (fechaNacimiento is null)
            return null;

        var hoy = DateOnly.FromDateTime(DateTime.UtcNow);
        var edad = hoy.Year - fechaNacimiento.Value.Year;

        if (fechaNacimiento.Value.AddYears(edad) > hoy)
            edad--;

        return edad;
    }
}
