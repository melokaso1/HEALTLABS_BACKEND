namespace Application.DTOs.Diagnostico
{
    public class CreateDiagnosticoDto
    {
        public string CodigoCie10 { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}
