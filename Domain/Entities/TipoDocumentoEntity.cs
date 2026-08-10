namespace Domain.Entities
{
    public class TipoDocumentoEntity
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        private TipoDocumentoEntity() { }

        public TipoDocumentoEntity(string codigo, string nombre)
        {
            Id = Guid.NewGuid();
            Codigo = codigo;
            Nombre = nombre;
        }

        public void Update(string codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;
        }
    }
}
