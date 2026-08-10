namespace Domain.Entities
{
    public class LoginIntentoEntity
    {
        public Guid Id { get; set; }
        public Guid? UsuarioId { get; set; }
        public string UsernameIntentado { get; set; } = null!;
        public bool Exitoso { get; set; }
        public string? MotivoFallo { get; set; }
        public string? Ip { get; set; }
        public string? UserAgent { get; set; }
        public DateTime Fecha { get; set; }

        public UsuarioEntity? Usuario { get; set; }

        private LoginIntentoEntity() { }

        public LoginIntentoEntity(
            Guid? usuarioId,
            string usernameIntentado,
            bool exitoso,
            string? motivoFallo,
            string? ip,
            string? userAgent,
            DateTime fecha)
        {
            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            UsernameIntentado = usernameIntentado;
            Exitoso = exitoso;
            MotivoFallo = motivoFallo;
            Ip = ip;
            UserAgent = userAgent;
            Fecha = fecha;
        }

        public void Update(
            Guid? usuarioId,
            string usernameIntentado,
            bool exitoso,
            string? motivoFallo,
            string? ip,
            string? userAgent,
            DateTime fecha)
        {
            UsuarioId = usuarioId;
            UsernameIntentado = usernameIntentado;
            Exitoso = exitoso;
            MotivoFallo = motivoFallo;
            Ip = ip;
            UserAgent = userAgent;
            Fecha = fecha;
        }
    }
}
