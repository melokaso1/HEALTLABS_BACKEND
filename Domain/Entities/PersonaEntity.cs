namespace Domain.Entities
{
    public class PersonaEntity
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public Guid? TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public string? Sexo { get; set; }

        private PersonaEntity() { }

        public PersonaEntity(string nombre, string apellido, Guid? tipoDocumentoId, string numeroDocumento,
            string? direccion, string? telefono, DateOnly? fechaNacimiento, string? sexo)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Apellido = apellido;
            TipoDocumentoId = tipoDocumentoId;
            NumeroDocumento = numeroDocumento;
            Direccion = direccion;
            Telefono = telefono;
            FechaNacimiento = fechaNacimiento;
            Sexo = sexo;
        }

        public void update(string nombre, string apellido, Guid? tipoDocumentoId, string numeroDocumento,
            string? direccion, string? telefono, DateOnly? fechaNacimiento, string? sexo)
        {
            Nombre = nombre;
            Apellido = apellido;
            TipoDocumentoId = tipoDocumentoId;
            NumeroDocumento = numeroDocumento;
            Direccion = direccion;
            Telefono = telefono;
            FechaNacimiento = fechaNacimiento;
            Sexo = sexo;
        }
    }
}
