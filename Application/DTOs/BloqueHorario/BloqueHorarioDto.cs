using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.BloqueHorario
{
    public class BloqueHorarioDto
    {
        public Guid IdBloque { get; set; }
        public Guid IdDisponibilidad { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public bool Ocupado { get; set; }
    }
}