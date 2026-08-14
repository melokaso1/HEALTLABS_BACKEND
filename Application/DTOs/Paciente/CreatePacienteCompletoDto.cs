using Application.DTOs.Persona;

namespace Application.DTOs.Paciente;

public sealed class CreatePacienteCompletoDto
{
    public CreatePersonaDto Persona { get; set; } = new();
    public string? Telefono { get; set; }
    public string? TipoTelefono { get; set; }
    public string? Direccion { get; set; }
    public string? Ciudad { get; set; }
    public string? TipoSangre { get; set; }
    public bool Activo { get; set; } = true;
}
