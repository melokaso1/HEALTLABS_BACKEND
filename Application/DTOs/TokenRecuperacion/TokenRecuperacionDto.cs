namespace Application.DTOs.TokenRecuperacion
{
    public class TokenRecuperacionDto
    {
        public Guid TokenRecuperacionId { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? UsadoEn { get; set; }
        public string? IpSolicitud { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
