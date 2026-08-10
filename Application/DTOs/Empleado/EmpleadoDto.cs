using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Empleado
{
    public class EmpleadoDto
    {
        public Guid IdEmpleado { get; set; }
        public Guid IdPersona { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdCargo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public bool Activo { get; set; }
    }
}