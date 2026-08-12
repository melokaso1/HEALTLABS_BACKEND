namespace Domain.Entities
{
    public class AntecedenteEntity
    {
        public Guid Id { get; set; }
        public Guid PacienteId { get; set; }
        public string Tipo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
        public Guid? UsuarioRegistroId { get; set; }

        public PacienteEntity? Paciente { get; set; }
        public UsuarioEntity? UsuarioRegistro { get; set; }

        private AntecedenteEntity() { }

        public AntecedenteEntity(
            Guid pacienteId,
            string tipo,
            string descripcion,
            Guid? usuarioRegistroId)
        {
            Id = Guid.NewGuid();
            PacienteId = pacienteId;
            Tipo = tipo;
            Descripcion = descripcion;
            FechaRegistro = DateTime.UtcNow;
            UsuarioRegistroId = usuarioRegistroId;
        }

        public void Update(
            string tipo,
            string descripcion,
            Guid? usuarioRegistroId)
        {
            Tipo = tipo;
            Descripcion = descripcion;
            UsuarioRegistroId = usuarioRegistroId;
        }
    }
}
