namespace Domain.Entities
{
    public class EstadoCita
    {
        public Guid IdEstadoCita { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public ICollection<Cita> Citas { get; set; } = [];
    }
}
