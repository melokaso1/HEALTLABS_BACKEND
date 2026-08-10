using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Paciente
{
    public class PacienteDto
    {
        public Guid IdPaciente { get; set; }
        public Guid IdPersona { get; set; }
        public bool Activo { get; set; }
    }
}