namespace Domain.Entities
{
    public class Disponibilidad
    {
        public int IdDisponibilidad { get; set; }
        public int IdMedico { get; set; }
        public int DiaSemana { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public bool Activo { get; set; }
        public DateOnly FechaInicioVigencia { get; set; }
        public DateOnly? FechaFinVigencia { get; set; }

        public ICollection<BloqueHorario> BloquesHorario { get; set; } = [];
    }
}
