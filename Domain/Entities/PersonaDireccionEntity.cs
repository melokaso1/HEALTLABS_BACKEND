namespace Domain.Entities
{
    public class PersonaDireccionEntity
    {
        public Guid Id { get; set; }
        public Guid PersonaId { get; set; }
        public string Direccion { get; set; } = null!;
        public string? Ciudad { get; set; }
        public string? Tipo { get; set; }
        public bool Principal { get; set; }

        public PersonaEntity? Persona { get; set; }

        private PersonaDireccionEntity() { }

        public PersonaDireccionEntity(
            Guid personaId,
            string direccion,
            string? ciudad,
            string? tipo,
            bool principal)
        {
            Id = Guid.NewGuid();
            PersonaId = personaId;
            Direccion = direccion;
            Ciudad = ciudad;
            Tipo = tipo;
            Principal = principal;
        }

        public void Update(
            string direccion,
            string? ciudad,
            string? tipo,
            bool principal)
        {
            Direccion = direccion;
            Ciudad = ciudad;
            Tipo = tipo;
            Principal = principal;
        }
    }
}
