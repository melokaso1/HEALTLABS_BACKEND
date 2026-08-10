using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.BloqueHorario
{
    public class BloqueHorarioDto
    {
        public Guid BloqueId { get; set; }
        public Guid DisponibilidadId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public bool Ocupado { get; set; }
    }
}