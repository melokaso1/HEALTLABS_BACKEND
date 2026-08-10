using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Diagnostico
{
    public class DiagnosticoDto
    {
        public Guid DiagnosticoId { get; set; }
        public string CodigoCie10 { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}