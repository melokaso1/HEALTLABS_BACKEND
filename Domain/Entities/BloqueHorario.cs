namespace Domain.Entities
{
    public class BloqueHorario
    {
        public int IdBloque { get; set; }
        public int IdDisponibilidad { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public bool Ocupado { get; set; }

        public Disponibilidad? Disponibilidad { get; set; }
    }
}
