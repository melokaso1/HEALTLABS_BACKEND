using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Rol
{
    public class CreateRolDto
    {
        public string NombreRol { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
}