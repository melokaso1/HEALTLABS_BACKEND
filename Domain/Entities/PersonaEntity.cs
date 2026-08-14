namespace Domain.Entities
{
    public class PersonaEntity
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? Email { get; set; }
        public Guid TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public DateOnly? FechaNacimiento { get; set; }
        public Guid? SexoId { get; set; }
        public DateTime FechaCreacion { get; set; }

        public TipoDocumentoEntity? TipoDocumento { get; set; }
        public SexoEntity? Sexo { get; set; }
        public ICollection<PersonaTelefonoEntity> Telefonos { get; set; } = [];
        public ICollection<PersonaDireccionEntity> Direcciones { get; set; } = [];

        private PersonaEntity() { }

        public PersonaEntity(
            string nombre,
            string apellido,
            Guid tipoDocumentoId,
            string numeroDocumento,
            DateOnly? fechaNacimiento,
            Guid? sexoId)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Apellido = apellido;
            TipoDocumentoId = tipoDocumentoId;
            NumeroDocumento = numeroDocumento;
            FechaNacimiento = fechaNacimiento;
            SexoId = sexoId;
            FechaCreacion = DateTime.UtcNow;
        }

        public void Update(
            string nombre,
            string apellido,
            Guid tipoDocumentoId,
            string numeroDocumento,
            DateOnly? fechaNacimiento,
            Guid? sexoId)
        {
            Nombre = nombre;
            Apellido = apellido;
            TipoDocumentoId = tipoDocumentoId;
            NumeroDocumento = numeroDocumento;
            FechaNacimiento = fechaNacimiento;
            SexoId = sexoId;
        }
    }
}
