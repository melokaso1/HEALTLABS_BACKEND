using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.DetalleDiagnostico
{
    public class DetalleDiagnosticoDto
    {
        public Guid IdDetalleDiagnostico { get; set; }
        public Guid IdDetalleCita { get; set; }
        public Guid IdDiagnostico { get; set; }
        public bool Principal { get; set; }
    }
}