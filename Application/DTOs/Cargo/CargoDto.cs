using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Cargo
{
    public class CargoDto
    {
        public Guid IdCargo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int NivelJerarquico { get; set; }
    }
}