using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Tratamiento
{
    public class CreateTratamientoDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Dosis { get; set; }
        public string? Frecuencia { get; set; }
        public int DuracionDias { get; set; }
        public string? Indicaciones { get; set; }
    }
}