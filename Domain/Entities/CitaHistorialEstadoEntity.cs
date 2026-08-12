namespace Domain.Entities
{
    public class CitaHistorialEstadoEntity
    {
        public Guid Id { get; set; }
        public Guid CitaId { get; set; }
        public Guid EstadoAnteriorId { get; set; }
        public Guid EstadoNuevoId { get; set; }
        public Guid? UsuarioId { get; set; }
        public DateTime FechaCambio { get; set; }
        public string? Observacion { get; set; }

        public CitaEntity? Cita { get; set; }
        public EstadoCitaEntity? EstadoAnterior { get; set; }
        public EstadoCitaEntity? EstadoNuevo { get; set; }
        public UsuarioEntity? Usuario { get; set; }

        private CitaHistorialEstadoEntity() { }

        public CitaHistorialEstadoEntity(
            Guid citaId,
            Guid estadoAnteriorId,
            Guid estadoNuevoId,
            Guid? usuarioId,
            string? observacion)
        {
            Id = Guid.NewGuid();
            CitaId = citaId;
            EstadoAnteriorId = estadoAnteriorId;
            EstadoNuevoId = estadoNuevoId;
            UsuarioId = usuarioId;
            FechaCambio = DateTime.UtcNow;
            Observacion = observacion;
        }
    }
}
