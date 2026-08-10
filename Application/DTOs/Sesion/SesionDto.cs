namespace Application.DTOs.Sesion
{
    public class SesionDto
    {
        public Guid SesionId { get; set; }
        public Guid UsuarioId { get; set; }
        public string Jti { get; set; } = string.Empty;
        public DateTime AccessExpiresAt { get; set; }
        public DateTime RefreshExpiresAt { get; set; }
        public DateTime EmitidoEn { get; set; }
        public DateTime? UltimoUso { get; set; }
        public DateTime? RevocadoEn { get; set; }
        public string? MotivoRevocacion { get; set; }
        public string? Ip { get; set; }
        public string? UserAgent { get; set; }
        public string? Dispositivo { get; set; }
    }
}
