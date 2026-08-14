namespace Application.DTOs.Paciente
{
    public class UpdatePacienteDto
    {
        public bool? Activo { get; set; }
        public string? TipoSangre { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public Guid? TipoDocumentoId { get; set; }
        public string? NumeroDocumento { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public Guid? SexoId { get; set; }
        public string? Telefono { get; set; }
        public string? TipoTelefono { get; set; }
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
    }
}
