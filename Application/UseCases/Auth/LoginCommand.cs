using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Auth
{

    
       public sealed record LoginCommand(string UsernameOrEmail, string Password);
       public sealed record LoginResult(string AccessToken, string RefreshToken, DateTime AccessTokenExpiresAt, DateTime RefreshTokenExpiresAt);

    
}
