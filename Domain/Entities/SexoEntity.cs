namespace Domain.Entities
{
    public class SexoEntity
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        private SexoEntity() { }

        public SexoEntity(string codigo, string nombre)
        {
            Id = Guid.NewGuid();
            Codigo = codigo;
            Nombre = nombre;
        }

        public void Update(string nombre)
        {
            Nombre = nombre;
        }
    }
}
