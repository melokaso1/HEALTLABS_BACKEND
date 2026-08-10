using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Medico
{
    public class CreateMedicoDto
    {
        public Guid IdEmpleado { get; set; }
        public Guid IdEspecialidad { get; set; }
    }
}