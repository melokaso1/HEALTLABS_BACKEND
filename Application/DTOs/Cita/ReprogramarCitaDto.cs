namespace Application.DTOs.Cita;

public class ReprogramarCitaDto
{
    public DateOnly Fecha { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public string? Observaciones { get; set; }
    public Guid? UsuarioId { get; set; }
}
