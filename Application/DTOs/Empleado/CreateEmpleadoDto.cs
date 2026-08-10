using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Empleado
{
    public class CreateEmpleadoDto
    {
        public Guid IdPersona { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdCargo { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}