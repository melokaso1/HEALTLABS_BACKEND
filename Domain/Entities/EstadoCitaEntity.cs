namespace Domain.Entities
{
    public class EstadoCitaEntity
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string? Descripcion { get; set; }

        private EstadoCitaEntity() { }

        public EstadoCitaEntity(string codigo, string? descripcion)
        {
            Id = Guid.NewGuid();
            Codigo = codigo;
            Descripcion = descripcion;
        }

        public void Update(string codigo, string? descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }
    }
}
