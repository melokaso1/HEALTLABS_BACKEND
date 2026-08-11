namespace Application.DTOs.Horario
{
    public class UpdateHorarioDto
    {
        public Guid MedicoId { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly HoraEntrada { get; set; }
        public TimeOnly HoraSalida { get; set; }
        public TimeOnly SalidaAlmuerzo { get; set; }
        public TimeOnly RetornoActividades { get; set; }
    }
}
