using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateAccessToken(UsuarioEntity usuario, string rolNombre, string jti);
        string GenerateRefreshToken();
    }
}
