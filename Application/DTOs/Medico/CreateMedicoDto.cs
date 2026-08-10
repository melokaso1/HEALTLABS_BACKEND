namespace Application.DTOs.Medico
{
    public class CreateMedicoDto
    {
        public Guid EmpleadoId { get; set; }
        public string? RegistroProfesional { get; set; }
        public bool Activo { get; set; } = true;
    }
}
