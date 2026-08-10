using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.TipoDocumento
{
    public class TipoDocumentoDto
    {
        public Guid IdTipoDocumento { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}