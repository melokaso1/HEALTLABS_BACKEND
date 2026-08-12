namespace Application.DTOs.PersonaDireccion
{
    public class UpdatePersonaDireccionDto
    {
        public string Direccion { get; set; } = string.Empty;
        public string? Ciudad { get; set; }
        public bool Principal { get; set; }
    }
}
