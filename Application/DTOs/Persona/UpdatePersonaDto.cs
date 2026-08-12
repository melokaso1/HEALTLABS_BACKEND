namespace Application.DTOs.Persona
{
    public class UpdatePersonaDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public Guid TipoDocumentoId { get; set; }
        public string? NumeroDocumento { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public Guid? SexoId { get; set; }
    }
}
