namespace Domain.Entities
{
    public class HorarioEntity
    {
        public Guid Id { get; set; }
        public Guid MedicoId { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly HoraEntrada { get; set; }
        public TimeOnly HoraSalida { get; set; }
        public TimeOnly SalidaAlmuerzo { get; set; }
        public TimeOnly RetornoActividades { get; set; }

        public MedicoEntity? Medico { get; set; }

        private HorarioEntity() { }

        public HorarioEntity(
            Guid medicoId,
            DateOnly fecha,
            TimeOnly horaEntrada,
            TimeOnly horaSalida,
            TimeOnly salidaAlmuerzo,
            TimeOnly retornoActividades)
        {
            Id = Guid.NewGuid();
            MedicoId = medicoId;
            Fecha = fecha;
            HoraEntrada = horaEntrada;
            HoraSalida = horaSalida;
            SalidaAlmuerzo = salidaAlmuerzo;
            RetornoActividades = retornoActividades;
        }

        public void Update(
            Guid medicoId,
            DateOnly fecha,
            TimeOnly horaEntrada,
            TimeOnly horaSalida,
            TimeOnly salidaAlmuerzo,
            TimeOnly retornoActividades)
        {
            MedicoId = medicoId;
            Fecha = fecha;
            HoraEntrada = horaEntrada;
            HoraSalida = horaSalida;
            SalidaAlmuerzo = salidaAlmuerzo;
            RetornoActividades = retornoActividades;
        }
    }
}
