using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.DetalleDiagnostico
{
    public class DetalleDiagnosticoDto
    {
        public Guid DetalleDiagnosticoId { get; set; }
        public Guid DetalleCitaId { get; set; }
        public Guid DiagnosticoId { get; set; }
        public bool Principal { get; set; }
    }
}