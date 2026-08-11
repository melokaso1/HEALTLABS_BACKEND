namespace Application.DTOs.Permiso
{
    public class UpdatePermisoDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Modulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
}
