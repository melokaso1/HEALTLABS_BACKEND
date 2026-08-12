namespace Application.DTOs.Reportes;

public class CitaReporteDto
{
    public Guid CitaId { get; set; }
    public DateOnly Fecha { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public Guid MedicoId { get; set; }
    public Guid PacienteId { get; set; }
    public Guid EstadoCitaId { get; set; }
    public string? EstadoCodigo { get; set; }
    public string MotivoConsulta { get; set; } = string.Empty;
}

public class ConteoPorEstadoDto
{
    public Guid EstadoCitaId { get; set; }
    public string? EstadoCodigo { get; set; }
    public int Total { get; set; }
}

public class ConteoPorMedicoDto
{
    public Guid MedicoId { get; set; }
    public int Total { get; set; }
}
