namespace Domain.Entities
{
    public class PacienteAlergiaEntity
    {
        public Guid Id { get; set; }
        public Guid PacienteId { get; set; }
        public string Sustancia { get; set; } = null!;
        public string? Reaccion { get; set; }
        public string? Severidad { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }

        public PacienteEntity? Paciente { get; set; }

        private PacienteAlergiaEntity() { }

        public PacienteAlergiaEntity(
            Guid pacienteId,
            string sustancia,
            string? reaccion,
            string? severidad,
            bool activo)
        {
            Id = Guid.NewGuid();
            PacienteId = pacienteId;
            Sustancia = sustancia;
            Reaccion = reaccion;
            Severidad = severidad;
            Activo = activo;
            FechaRegistro = DateTime.UtcNow;
        }

        public void Update(
            Guid pacienteId,
            string sustancia,
            string? reaccion,
            string? severidad,
            bool activo)
        {
            PacienteId = pacienteId;
            Sustancia = sustancia;
            Reaccion = reaccion;
            Severidad = severidad;
            Activo = activo;
        }
    }
}
