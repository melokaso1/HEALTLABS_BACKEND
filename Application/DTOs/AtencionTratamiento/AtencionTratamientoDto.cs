using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.AtencionTratamiento
{
    public class AtencionTratamientoDto
    {
        public Guid AtencionTratamientoId { get; set; }
        public Guid DetalleCitaId { get; set; }
        public Guid TratamientoId { get; set; }
        public string? DosisPersonalizada { get; set; }
        public string? FrecuenciaPersonalizada { get; set; }
        public int DuracionDiasPersonalizada { get; set; }
        public string? IndicacionesPersonalizadas { get; set; }
    }
}