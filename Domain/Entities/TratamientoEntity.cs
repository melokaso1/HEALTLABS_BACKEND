namespace Domain.Entities
{
    public class TratamientoEntity
    {
        public Guid Id { get; private set; }
        public string Codigo { get; private set; } = null!;
        public string Nombre { get; private set; } = null!;
        public string? Descripcion { get; private set; }
        public string? Dosis { get; private set; }
        public string? Frecuencia { get; private set; }
        public int? DuracionDias { get; private set; }
        public string? Indicaciones { get; private set; }
        public bool Activo { get; private set; }

        public ICollection<AtencionTratamientoEntity> AtencionesTratamiento { get; private set; } = [];

        private TratamientoEntity() { }

        public TratamientoEntity(string codigo, string nombre, string? descripcion,
            string? dosis, string? frecuencia, int? duracionDias, string? indicaciones, bool activo)
        {
            Id = Guid.NewGuid();
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            Dosis = dosis;
            Frecuencia = frecuencia;
            DuracionDias = duracionDias;
            Indicaciones = indicaciones;
            Activo = activo;
        }

        public void Update(string codigo, string nombre, string? descripcion,
            string? dosis, string? frecuencia, int? duracionDias, string? indicaciones, bool activo)
        {
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            Dosis = dosis;
            Frecuencia = frecuencia;
            DuracionDias = duracionDias;
            Indicaciones = indicaciones;
            Activo = activo;
        }
    }
}
