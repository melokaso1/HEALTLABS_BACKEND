namespace Application.DTOs.Cita;

public class CancelCitaDto
{
    public string MotivoCancelacion { get; set; } = "Cancelada por usuario";
    public Guid? UsuarioCancelacionId { get; set; }
}
