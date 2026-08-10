namespace Application.DTOs.CitaHistorialEstado
{
    public class CreateCitaHistorialEstadoDto
    {
        public Guid CitaId { get; set; }
        public Guid EstadoAnteriorId { get; set; }
        public Guid EstadoNuevoId { get; set; }
        public Guid? UsuarioId { get; set; }
        public string? Observacion { get; set; }
    }
}
