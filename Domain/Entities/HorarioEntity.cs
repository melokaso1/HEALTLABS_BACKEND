namespace Domain.Entities
{
    public class HorarioEntity
    {
        public Guid Id { get; set; }
        public Guid MedicoId { get; set; }
        public TimeOnly HoraEntrada { get; set; }
        public TimeOnly HoraSalida { get; set; }
        public TimeOnly SalidaAlmuerzo { get; set; }
        public TimeOnly RetornoActividades { get; set; }

        public MedicoEntity? Medico { get; set; }

        private HorarioEntity() { }

        public HorarioEntity(
            Guid medicoId,
            TimeOnly horaEntrada,
            TimeOnly horaSalida,
            TimeOnly salidaAlmuerzo,
            TimeOnly retornoActividades)
        {
            Id = Guid.NewGuid();
            MedicoId = medicoId;
            HoraEntrada = horaEntrada;
            HoraSalida = horaSalida;
            SalidaAlmuerzo = salidaAlmuerzo;
            RetornoActividades = retornoActividades;
        }

        public void Update(
            TimeOnly horaEntrada,
            TimeOnly horaSalida,
            TimeOnly salidaAlmuerzo,
            TimeOnly retornoActividades)
        {
            HoraEntrada = horaEntrada;
            HoraSalida = horaSalida;
            SalidaAlmuerzo = salidaAlmuerzo;
            RetornoActividades = retornoActividades;
        }
    }
}
