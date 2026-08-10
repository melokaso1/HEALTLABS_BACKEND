namespace Domain.Entities
{
    public class Tratamiento
    {
        public Guid IdTratamiento { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Dosis { get; set; }
        public string? Frecuencia { get; set; }
        public int? DuracionDias { get; set; }
        public string? Indicaciones { get; set; }
        public bool Activo { get; set; }

        public ICollection<AtencionTratamiento> AtencionesTratamiento { get; set; } = [];
    }
}
