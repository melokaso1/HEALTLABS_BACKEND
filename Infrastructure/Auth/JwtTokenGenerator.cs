using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth
{
    public sealed class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _settings;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            var durationInMinutes = int.TryParse(configuration["JWT:DurationInMinutes"], out var configuredDuration)
                ? configuredDuration
                : 15;

            _settings = new JwtSettings(
                configuration["JWT:Key"],
                configuration["JWT:Issuer"],
                configuration["JWT:Audience"],
                durationInMinutes);

            _settings.Validate();
        }

        public string GenerateAccessToken(UsuarioEntity usuario, string rolNombre, string jti)
        {
            ArgumentNullException.ThrowIfNull(usuario);
            ArgumentException.ThrowIfNullOrWhiteSpace(rolNombre);
            ArgumentException.ThrowIfNullOrWhiteSpace(jti);

            var now = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new(ClaimTypes.Name, usuario.Username),
                new(JwtRegisteredClaimNames.Email, usuario.Email),
                new(ClaimTypes.Role, rolNombre),
                new(JwtRegisteredClaimNames.Jti, jti),
                new("ver", usuario.TokenVersion.ToString()),
                new(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(now).ToString(), ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_settings.DurationInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}
