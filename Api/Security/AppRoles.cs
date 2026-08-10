using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Security
{
    public static class AppRoles
    {
        public const string Admin = "Administrador";
        public const string Medico = "Profesional";
        public const string Recepcionista = "Recepcionista";

        public const string Staff = $"{Admin},{Recepcionista}";
        public const string Todos = $"{Admin},{Medico},{Recepcionista}";
    }
}