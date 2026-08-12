namespace Application.DTOs.Cita;

public class CancelCitaDto
{
    public Guid CitaId { get; set; }
    public string MotivoCancelacion { get; set; } = "Cancelada por usuario";
    public Guid? UsuarioCancelacionId { get; set; }
}
