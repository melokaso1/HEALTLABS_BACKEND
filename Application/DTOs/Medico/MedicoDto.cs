using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Medico
{
    public class MedicoDto
    {
        public Guid MedicoId { get; set; }
        public Guid EmpleadoId { get; set; }
        public Guid EspecialidadId { get; set; }
    }
}