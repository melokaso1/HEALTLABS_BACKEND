namespace Application.DTOs.Paciente
{
    public class UpdatePacienteDto
    {
        public bool Activo { get; set; } = true;
        public string? TipoSangre { get; set; }
    }
}
