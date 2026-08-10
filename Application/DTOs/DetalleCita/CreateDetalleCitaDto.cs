using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.DetalleCita
{
    public class CreateDetalleCitaDto
    {
        public Guid IdCita { get; set; }
        public Guid IdProfesional { get; set; }
        public string? NotaAtencion { get; set; }
        public string? ResumenConsulta { get; set; }
    }
}