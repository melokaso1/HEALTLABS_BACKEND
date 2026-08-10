namespace Domain.Entities
{
    public class HorarioEntity
    {
        public Guid Id { get; private set; }
        public DateOnly Fecha { get; private set; }
        public TimeOnly HoraInicio { get; private set; }
        public TimeOnly HoraFin { get; private set; }
        public TimeOnly Retorno { get; private set; }
        public bool Descanso { get; private set; }

        //public MedicoEntity? Medico { get; private set; } // Falta entidad de medico

        private HorarioEntity() { }

        public HorarioEntity(DateOnly fecha, TimeOnly horaInicio, TimeOnly horaFin,
            TimeOnly retorno, bool descanso)
        {
            Id = Guid.NewGuid();
            Fecha = fecha;
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            Retorno = retorno;
            Descanso = descanso;
        }

        public void Update(DateOnly fecha, TimeOnly horaInicio, TimeOnly horaFin,
            TimeOnly retorno, bool descanso)
        {
            Fecha = fecha;
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            Retorno = retorno;
            Descanso = descanso;
        }
    }
}
