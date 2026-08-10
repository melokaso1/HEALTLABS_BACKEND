using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Empleado
{
    public class EmpleadoDto
    {
        public Guid EmpleadoId { get; set; }
        public Guid PersonaId { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid CargoId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public bool Activo { get; set; }
    }
}