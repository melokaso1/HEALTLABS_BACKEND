namespace Application.DTOs.Horario
{
    public class UpdateHorarioDto
    {
        public TimeOnly HoraEntrada { get; set; }
        public TimeOnly HoraSalida { get; set; }
        public TimeOnly SalidaAlmuerzo { get; set; }
        public TimeOnly RetornoActividades { get; set; }
    }
}