namespace Application.DTOs.PersonaDireccion
{
    public class UpdatePersonaDireccionDto
    {
        public Guid PersonaId { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string? Ciudad { get; set; }
        public string? Tipo { get; set; }
        public bool Principal { get; set; }
    }
}
