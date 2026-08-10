namespace Domain.Entities
{
    public class Horario
    {
        public Guid IdHorario { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public TimeOnly Retorno { get; set; }
        public bool Descanso { get; set; }

        public Medico? Medico { get; set; }
    }
}
