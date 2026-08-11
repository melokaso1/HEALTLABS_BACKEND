namespace Application.DTOs.PacienteAlergia
{
    public class UpdatePacienteAlergiaDto
    {
        public Guid PacienteId { get; set; }
        public string Sustancia { get; set; } = string.Empty;
        public string? Reaccion { get; set; }
        public string? Severidad { get; set; }
        public bool Activo { get; set; } = true;
    }
}
