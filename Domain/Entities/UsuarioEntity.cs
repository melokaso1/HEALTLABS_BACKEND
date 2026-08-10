namespace Domain.Entities
{
    public class UsuarioEntity
    {
        public Guid Id { get; set; }
        public Guid? RolId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool Activo { get; set; }
        public string? JwtCode { get; set; }

        private UsuarioEntity() { }

        public UsuarioEntity(Guid? rolId, string email, string password, bool activo, string? jwtCode)
        {
            Id = Guid.NewGuid();
            RolId = rolId;
            Email = email;
            Password = password;
            Activo = activo;
            JwtCode = jwtCode;
        }

        public void update(Guid? rolId, string email, string password, bool activo, string? jwtCode)
        {
            RolId = rolId;
            Email = email;
            Password = password;
            Activo = activo;
            JwtCode = jwtCode;
        }
    }
}

