namespace Domain.Entities
{
    public class TipoDocumentoEntity
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;

        private TipoDocumentoEntity() { }

        public TipoDocumentoEntity(string nombre)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
        }

        public void update(string nombre)
        {
            Nombre = nombre;
        }
    }
}
