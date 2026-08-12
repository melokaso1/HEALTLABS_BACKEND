namespace Domain.Entities
{
    public class EmpleadoEntity
    {
        public Guid Id { get; set; }
        public Guid PersonaId { get; set; }
        public Guid CargoId { get; set; }
        public DateOnly FechaIngreso { get; set; }
        public DateOnly? FechaRetiro { get; set; }
        public bool Activo { get; set; }

        public PersonaEntity? Persona { get; set; }
        public CargoEntity? Cargo { get; set; }
        public UsuarioEntity? Usuario { get; set; }
        public MedicoEntity? Medico { get; set; }

        private EmpleadoEntity() { }

        public EmpleadoEntity(
            Guid personaId,
            Guid cargoId,
            DateOnly fechaIngreso,
            DateOnly? fechaRetiro,
            bool activo)
        {
            Id = Guid.NewGuid();
            PersonaId = personaId;
            CargoId = cargoId;
            FechaIngreso = fechaIngreso;
            FechaRetiro = fechaRetiro;
            Activo = activo;
        }

        public void Update(
            Guid cargoId,
            DateOnly fechaIngreso,
            DateOnly? fechaRetiro,
            bool activo)
        {
            CargoId = cargoId;
            FechaIngreso = fechaIngreso;
            FechaRetiro = fechaRetiro;
            Activo = activo;
        }
    }
}
