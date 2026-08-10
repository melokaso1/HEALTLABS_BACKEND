namespace Application.DTOs.Cargo
{
    public class CreateCargoDto
    {
        public string? Codigo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int? NivelJerarquico { get; set; }
    }
}
