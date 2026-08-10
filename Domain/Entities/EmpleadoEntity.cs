namespace Domain.Entities
{
    public class EmpleadoEntity
    {
        public int IdEmpleado { get; set; }
        public int? IdPersona { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdCargo { get; set; }
        public DateOnly? FechaIngreso { get; set; }
        public DateOnly? FechaRetiro { get; set; }
        public bool Activo { get; set; }

        public PersonaEntity? Persona { get; set; }
        public UsuarioEntity? Usuario { get; set; }
        public CargoEntity? Cargo { get; set; }
        public ICollection<MedicoEntity> Medicos { get; set; } = [];
    }
}
