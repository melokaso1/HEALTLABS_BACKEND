namespace Domain.Entities
{
    public class MedicoEntity
    {
        public Guid Id { get; set; }
        public Guid EmpleadoId { get; set; }
        public string? RegistroProfesional { get; set; }
        public bool Activo { get; set; }

        public EmpleadoEntity? Empleado { get; set; }
        public ICollection<MedicoEspecialidadEntity> Especialidades { get; set; } = [];
        public ICollection<HorarioEntity> Horarios { get; set; } = [];

        private MedicoEntity() { }

        public MedicoEntity(Guid empleadoId, string? registroProfesional, bool activo)
        {
            Id = Guid.NewGuid();
            EmpleadoId = empleadoId;
            RegistroProfesional = registroProfesional;
            Activo = activo;
        }

        public void Update(Guid empleadoId, string? registroProfesional, bool activo)
        {
            EmpleadoId = empleadoId;
            RegistroProfesional = registroProfesional;
            Activo = activo;
        }
    }
}
