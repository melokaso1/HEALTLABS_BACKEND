namespace Application.DTOs.Tratamiento
{
    public class CreateTratamientoDto
    {
        public string? Codigo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;
    }
}
