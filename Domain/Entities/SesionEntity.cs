namespace Domain.Entities
{
    public class SesionEntity
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Jti { get; set; } = null!;
        public string RefreshTokenHash { get; set; } = null!;
        public DateTime AccessExpiresAt { get; set; }
        public DateTime RefreshExpiresAt { get; set; }
        public DateTime EmitidoEn { get; set; }
        public DateTime? UltimoUso { get; set; }
        public DateTime? RevocadoEn { get; set; }
        public string? MotivoRevocacion { get; set; }
        public string? Ip { get; set; }
        public string? UserAgent { get; set; }
        public string? Dispositivo { get; set; }

        public UsuarioEntity? Usuario { get; set; }

        private SesionEntity() { }

        public SesionEntity(
            Guid usuarioId,
            string jti,
            string refreshTokenHash,
            DateTime accessExpiresAt,
            DateTime refreshExpiresAt,
            DateTime emitidoEn,
            DateTime? ultimoUso,
            DateTime? revocadoEn,
            string? motivoRevocacion,
            string? ip,
            string? userAgent,
            string? dispositivo)
        {
            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            Jti = jti;
            RefreshTokenHash = refreshTokenHash;
            AccessExpiresAt = accessExpiresAt;
            RefreshExpiresAt = refreshExpiresAt;
            EmitidoEn = emitidoEn;
            UltimoUso = ultimoUso;
            RevocadoEn = revocadoEn;
            MotivoRevocacion = motivoRevocacion;
            Ip = ip;
            UserAgent = userAgent;
            Dispositivo = dispositivo;
        }

        public void Update(
            Guid usuarioId,
            string jti,
            string refreshTokenHash,
            DateTime accessExpiresAt,
            DateTime refreshExpiresAt,
            DateTime emitidoEn,
            DateTime? ultimoUso,
            DateTime? revocadoEn,
            string? motivoRevocacion,
            string? ip,
            string? userAgent,
            string? dispositivo)
        {
            UsuarioId = usuarioId;
            Jti = jti;
            RefreshTokenHash = refreshTokenHash;
            AccessExpiresAt = accessExpiresAt;
            RefreshExpiresAt = refreshExpiresAt;
            EmitidoEn = emitidoEn;
            UltimoUso = ultimoUso;
            RevocadoEn = revocadoEn;
            MotivoRevocacion = motivoRevocacion;
            Ip = ip;
            UserAgent = userAgent;
            Dispositivo = dispositivo;
        }
        public void Revocar(string motivo)
        {
            RevocadoEn = DateTime.UtcNow;
            MotivoRevocacion = motivo;
        }
    }
}