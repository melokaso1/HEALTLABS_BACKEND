namespace Domain.Entities
{
    public class TipoCitaEntity
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int DuracionMinutos { get; set; }
        public bool Activo { get; set; }

        private TipoCitaEntity() { }

        public TipoCitaEntity(string codigo, string nombre, int duracionMinutos, bool activo)
        {
            Id = Guid.NewGuid();
            Codigo = codigo;
            Nombre = nombre;
            DuracionMinutos = duracionMinutos;
            Activo = activo;
        }

        public void Update(string codigo, string nombre, int duracionMinutos, bool activo)
        {
            Codigo = codigo;
            Nombre = nombre;
            DuracionMinutos = duracionMinutos;
            Activo = activo;
        }
    }
}
