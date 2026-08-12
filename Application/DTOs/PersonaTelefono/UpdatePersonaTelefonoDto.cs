namespace Application.DTOs.PersonaTelefono
{
    public class UpdatePersonaTelefonoDto
    {
        public string Telefono { get; set; } = string.Empty;
        public string? Tipo { get; set; }
        public bool Principal { get; set; }
    }
}
