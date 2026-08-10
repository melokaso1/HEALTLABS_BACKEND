namespace Domain.Entities
{
    public class PersonaTelefonoEntity
    {
        public Guid Id { get; set; }
        public Guid PersonaId { get; set; }
        public string Telefono { get; set; } = null!;
        public string? Tipo { get; set; }
        public bool Principal { get; set; }

        public PersonaEntity? Persona { get; set; }

        private PersonaTelefonoEntity() { }

        public PersonaTelefonoEntity(Guid personaId, string telefono, string? tipo, bool principal)
        {
            Id = Guid.NewGuid();
            PersonaId = personaId;
            Telefono = telefono;
            Tipo = tipo;
            Principal = principal;
        }

        public void Update(Guid personaId, string telefono, string? tipo, bool principal)
        {
            PersonaId = personaId;
            Telefono = telefono;
            Tipo = tipo;
            Principal = principal;
        }
    }
}
