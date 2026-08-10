namespace Domain.Entities
{
    public class HorarioEntity
    {
        public Guid IdHorario { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public TimeOnly Retorno { get; set; }
        public bool Descanso { get; set; }

        public MedicoEntity? Medico { get; set; }
    }
}
