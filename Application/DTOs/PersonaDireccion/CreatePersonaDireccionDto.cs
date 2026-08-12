namespace Application.DTOs.PersonaDireccion
{
    public class CreatePersonaDireccionDto
    {
        public Guid PersonaId { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string? Ciudad { get; set; }
        public bool Principal { get; set; }
    }
}
