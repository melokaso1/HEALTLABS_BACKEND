namespace Application.DTOs.Paciente
{
    public class UpdatePacienteDto
    {
        public Guid PersonaId { get; set; }
        public bool Activo { get; set; } = true;
    }
}
