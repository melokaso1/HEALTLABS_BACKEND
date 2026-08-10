namespace Application.DTOs.Permiso
{
    public class CreatePermisoDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Modulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
}
