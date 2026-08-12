namespace Application.DTOs.Diagnostico
{
    public class UpdateDiagnosticoDto
    {
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}
