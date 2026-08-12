namespace Application.DTOs.Medico
{
    public class UpdateMedicoDto
    {
        public string? RegistroProfesional { get; set; }
        public bool Activo { get; set; } = true;
    }
}
