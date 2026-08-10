using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Usuario
{
    public class UsuarioDto
    {
        public Guid IdUsuario { get; set; }
        public Guid IdRol { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}