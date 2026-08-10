using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.EstadoCita
{
    public class EstadoCitaDto
    {
        public Guid EstadoCitaId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
}