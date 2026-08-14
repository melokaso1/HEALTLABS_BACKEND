namespace Domain.Entities
{
    public class PacienteEntity
    {
        public Guid Id { get; set; }
        public Guid PersonaId { get; set; }
        public bool Activo { get; set; }
        public string? TipoSangre { get; set; }
        public DateTime FechaRegistro { get; set; }

        public PersonaEntity? Persona { get; set; }
        public ICollection<AntecedenteEntity> Antecedentes { get; set; } = [];
        public ICollection<PacienteAlergiaEntity> Alergias { get; set; } = [];

        private PacienteEntity() { }

        public PacienteEntity(Guid personaId, bool activo, string? tipoSangre)
        {
            Id = Guid.NewGuid();
            PersonaId = personaId;
            Activo = activo;
            TipoSangre = tipoSangre;
            FechaRegistro = DateTime.UtcNow;
        }

        public void Update(bool activo, string? tipoSangre)
        {
            Activo = activo;
            TipoSangre = tipoSangre;
        }
    }
}
