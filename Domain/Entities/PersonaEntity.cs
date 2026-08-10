namespace Domain.Entities
{
    public class PersonaEntity
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public int? IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public string? Sexo { get; set; }

        public TipoDocumentoEntity? TipoDocumento { get; set; }
        public ICollection<EmpleadoEntity> Empleados { get; set; } = [];
        public ICollection<PacienteEntity> Pacientes { get; set; } = [];
    }
}
