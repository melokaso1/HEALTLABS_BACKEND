namespace Domain.Entities
{
    public class Cargo
    {
        public int IdCargo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public int? NivelJerarquico { get; set; }

        public ICollection<Empleado> Empleados { get; set; } = [];
    }
}
