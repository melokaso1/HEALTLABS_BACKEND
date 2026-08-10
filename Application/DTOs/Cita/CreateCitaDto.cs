using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Cita
{
    public class CreateCitaDto
    {
        public Guid IdPaciente { get; set; }
        public Guid IdMedico { get; set; }
        public Guid IdEstadoCita { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string? MotivoConsulta { get; set; }
        public string? Observaciones { get; set; }
        public Guid IdUsuarioCreacion { get; set; }
    }
}