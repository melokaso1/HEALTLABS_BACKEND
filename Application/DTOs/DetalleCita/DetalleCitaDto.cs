using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.DetalleCita
{
    public class DetalleCitaDto
    {
        public Guid DetalleId { get; set; }
        public Guid CitaId { get; set; }
        public Guid ProfesionalId { get; set; }
        public string? NotaAtencion { get; set; }
        public string? ResumenConsulta { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}