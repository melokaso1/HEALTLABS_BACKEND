namespace Application.DTOs.PacienteAlergia
{
    public class UpdatePacienteAlergiaDto
    {
        public string Sustancia { get; set; } = string.Empty;
        public string? Reaccion { get; set; }
        public string? Severidad { get; set; }
        public bool Activo { get; set; } = true;
    }
}
