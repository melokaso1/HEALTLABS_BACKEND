namespace Domain.Entities
{
    public class TokenRecuperacionEntity
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string TokenHash { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public DateTime? UsadoEn { get; set; }
        public string? IpSolicitud { get; set; }
        public DateTime FechaCreacion { get; set; }

        public UsuarioEntity? Usuario { get; set; }

        private TokenRecuperacionEntity() { }

        public TokenRecuperacionEntity(
            Guid usuarioId,
            string tokenHash,
            DateTime expiresAt,
            DateTime? usadoEn,
            string? ipSolicitud)
        {
            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            TokenHash = tokenHash;
            ExpiresAt = expiresAt;
            UsadoEn = usadoEn;
            IpSolicitud = ipSolicitud;
            FechaCreacion = DateTime.UtcNow;
        }

        public void Update(
            Guid usuarioId,
            string tokenHash,
            DateTime expiresAt,
            DateTime? usadoEn,
            string? ipSolicitud)
        {
            UsuarioId = usuarioId;
            TokenHash = tokenHash;
            ExpiresAt = expiresAt;
            UsadoEn = usadoEn;
            IpSolicitud = ipSolicitud;
        }
    }
}
