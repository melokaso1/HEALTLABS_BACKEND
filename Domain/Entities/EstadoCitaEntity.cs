namespace Domain.Entities
{
    public class EstadoCitaEntity
    {
        public Guid IdEstadoCita { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public ICollection<CitaEntity> Citas { get; set; } = [];
    }
}
