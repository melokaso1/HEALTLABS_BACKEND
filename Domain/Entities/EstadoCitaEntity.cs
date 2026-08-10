namespace Domain.Entities
{
    public class EstadoCitaEntity
    {
        public Guid Id { get; private set; }
        public string Codigo { get; private set; } = null!;
        public string Descripcion { get; private set; } = null!;

        public ICollection<CitaEntity> Citas { get; private set; } = [];

        private EstadoCitaEntity() { }

        public EstadoCitaEntity(string codigo, string descripcion)
        {
            Id = Guid.NewGuid();
            Codigo = codigo;
            Descripcion = descripcion;
        }

        public void Update(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }
    }
}
