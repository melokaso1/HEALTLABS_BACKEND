namespace Application.DTOs.Cargo
{
    public class UpdateCargoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int? NivelJerarquico { get; set; }
    }
}
