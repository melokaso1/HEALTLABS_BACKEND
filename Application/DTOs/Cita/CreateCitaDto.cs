using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Cita
{
    public class CreateCitaDto
    {
        public Guid PacienteId { get; set; }
        public Guid MedicoId { get; set; }
        public Guid EstadoCitaId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string? MotivoConsulta { get; set; }
        public string? Observaciones { get; set; }
        public Guid IdUsuarioCreacion { get; set; }
    }
}