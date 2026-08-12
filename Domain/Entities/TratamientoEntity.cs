namespace Domain.Entities
{
    public class TratamientoEntity
    {
        public Guid Id { get; set; }
        public string? Codigo { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }

        public TratamientoPosologiaEntity? Posologia { get; set; }

        private TratamientoEntity() { }

        public TratamientoEntity(string? codigo, string nombre, string? descripcion, bool activo)
        {
            Id = Guid.NewGuid();
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            Activo = activo;
        }

        public void Update(string nombre, string? descripcion, bool activo)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Activo = activo;
        }
    }
}
