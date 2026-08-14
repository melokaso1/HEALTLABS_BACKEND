namespace Application.DTOs.Paciente
{
    public class CreatePacienteDto
    {
        public Guid PersonaId { get; set; }
        public bool Activo { get; set; } = true;
        public string? TipoSangre { get; set; }
    }
}
