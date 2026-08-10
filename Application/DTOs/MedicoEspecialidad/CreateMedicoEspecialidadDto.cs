namespace Application.DTOs.MedicoEspecialidad
{
    public class CreateMedicoEspecialidadDto
    {
        public Guid MedicoId { get; set; }
        public Guid EspecialidadId { get; set; }
        public bool Principal { get; set; }
    }
}
