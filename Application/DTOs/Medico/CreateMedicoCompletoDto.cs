using Application.DTOs.Persona;

namespace Application.DTOs.Medico;

public sealed class CreateMedicoCompletoDto
{
    public CreatePersonaDto Persona { get; set; } = new();
    public Guid CargoId { get; set; }
    public DateOnly FechaIngreso { get; set; }
    public DateOnly? FechaRetiro { get; set; }
    public bool EmpleadoActivo { get; set; } = true;
    public string? RegistroProfesional { get; set; }
    public bool MedicoActivo { get; set; } = true;
    public Guid? EspecialidadId { get; set; }
    public bool EspecialidadPrincipal { get; set; } = true;
}
