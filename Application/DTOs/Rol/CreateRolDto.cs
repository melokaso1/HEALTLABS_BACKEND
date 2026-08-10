namespace Application.DTOs.Rol
{
    public class CreateRolDto
    {
        public string NombreRol { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;
    }
}
