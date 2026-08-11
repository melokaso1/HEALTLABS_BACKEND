using Api.Security;
using Application.DTOs.Auth;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Username == request.UsernameOrEmail || u.Email == request.UsernameOrEmail);

        var ahora = DateTime.UtcNow;

        if (usuario is null)
        {
            var intentoFallido = new LoginIntentoEntity(
                usuarioId: null,
                usernameIntentado: request.UsernameOrEmail,
                exitoso: false,
                motivoFallo: "Usuario no encontrado",
                ip: request.IpAddress,
                userAgent: request.UserAgent,
                fecha: ahora
            );

            _context.LoginIntentos.Add(intentoFallido);
            await _context.SaveChangesAsync();

            return BadRequest("Credenciales inválidas.");
        }

        if (!usuario.Activo)
        {
            var intentoFallido = new LoginIntentoEntity(
                usuarioId: usuario.Id,
                usernameIntentado: request.UsernameOrEmail,
                exitoso: false,
                motivoFallo: "Usuario inactivo",
                ip: request.IpAddress,
                userAgent: request.UserAgent,
                fecha: ahora
            );

            _context.LoginIntentos.Add(intentoFallido);
            await _context.SaveChangesAsync();

            return BadRequest("La cuenta de usuario se encuentra inactiva.");
        }

        if (usuario.PasswordHash != request.Password)
        {
            usuario.IntentosFallidos += 1;
            
            var intentoFallido = new LoginIntentoEntity(
                usuarioId: usuario.Id,
                usernameIntentado: request.UsernameOrEmail,
                exitoso: false,
                motivoFallo: "Contraseña incorrecta",
                ip: request.IpAddress,
                userAgent: request.UserAgent,
                fecha: ahora
            );

            _context.LoginIntentos.Add(intentoFallido);
            await _context.SaveChangesAsync();

            return BadRequest("Credenciales inválidas.");
        }

        // Login Exitoso
        usuario.IntentosFallidos = 0;
        usuario.UltimoLogin = ahora;

        var intentoExitoso = new LoginIntentoEntity(
            usuarioId: usuario.Id,
            usernameIntentado: request.UsernameOrEmail,
            exitoso: true,
            motivoFallo: null,
            ip: request.IpAddress,
            userAgent: request.UserAgent,
            fecha: ahora
        );
        _context.LoginIntentos.Add(intentoExitoso);

        var jti = Guid.NewGuid().ToString("N");
        var refreshToken = Guid.NewGuid().ToString("N");

        var sesion = new SesionEntity(
            usuarioId: usuario.Id,
            jti: jti,
            refreshTokenHash: refreshToken,
            accessExpiresAt: ahora.AddHours(8),
            refreshExpiresAt: ahora.AddDays(7),
            emitidoEn: ahora,
            ultimoUso: ahora,
            revocadoEn: null,
            motivoRevocacion: null,
            ip: request.IpAddress,
            userAgent: request.UserAgent,
            dispositivo: null
        );
        _context.Sesiones.Add(sesion);

        await _context.SaveChangesAsync();

        var response = new LoginResponseDto
        {
            UsuarioId = usuario.Id,
            SesionId = sesion.Id,
            Username = usuario.Username,
            Email = usuario.Email,
            RolNombre = usuario.Rol?.NombreRol ?? "Sin Rol",
            DebeCambiarPassword = usuario.DebeCambiarPassword,
            SesionExpiresAt = sesion.AccessExpiresAt,
            SessionToken = jti
        };

        return Ok(response);
    }

    [HttpPost("solicitar-recuperacion")]
    [AllowAnonymous]
    public async Task<IActionResult> SolicitarRecuperacion([FromBody] SolicitarRecuperacionDto request)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == request.EmailOrUsername || u.Username == request.EmailOrUsername);

        if (usuario is null)
        {
            return Ok(new { mensaje = "Si la cuenta existe en el sistema, se ha generado el token de recuperación." });
        }

        var tokenRaw = Guid.NewGuid().ToString("N");
        var tokenEntity = new TokenRecuperacionEntity(
            usuarioId: usuario.Id,
            tokenHash: tokenRaw,
            expiresAt: DateTime.UtcNow.AddHours(24),
            usadoEn: null,
            ipSolicitud: request.IpAddress
        );

        _context.TokensRecuperacion.Add(tokenEntity);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            mensaje = "Token de recuperación generado exitosamente.",
            tokenId = tokenEntity.Id,
            tokenRaw = tokenRaw,
            expiresAt = tokenEntity.ExpiresAt
        });
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto request)
    {
        var token = await _context.TokensRecuperacion.FindAsync(request.TokenId);
        if (token is null || token.TokenHash != request.TokenRaw)
        {
            return BadRequest("El token de recuperación es inválido.");
        }

        if (token.UsadoEn.HasValue)
        {
            return BadRequest("El token de recuperación ya ha sido utilizado.");
        }

        if (token.ExpiresAt < DateTime.UtcNow)
        {
            return BadRequest("El token de recuperación ha expirado.");
        }

        var usuario = await _context.Usuarios.FindAsync(token.UsuarioId);
        if (usuario is null)
        {
            return BadRequest("Usuario no encontrado.");
        }

        var ahora = DateTime.UtcNow;
        token.Update(
            usuarioId: token.UsuarioId,
            tokenHash: token.TokenHash,
            expiresAt: token.ExpiresAt,
            usadoEn: ahora,
            ipSolicitud: token.IpSolicitud
        );

        usuario.Update(
            empleadoId: usuario.EmpleadoId,
            rolId: usuario.RolId,
            username: usuario.Username,
            email: usuario.Email,
            passwordHash: request.NewPassword,
            activo: usuario.Activo,
            ultimoLogin: usuario.UltimoLogin,
            intentosFallidos: 0,
            bloqueadoHasta: null,
            debeCambiarPassword: false,
            passwordChangedAt: ahora,
            tokenVersion: usuario.TokenVersion + 1
        );

        await _context.SaveChangesAsync();
        return Ok(new { mensaje = "La contraseña ha sido actualizada correctamente." });
    }

    [HttpPost("logout/{sesionId:guid}")]
    [Authorize]
    public async Task<IActionResult> Logout(Guid sesionId)
    {
        var sesion = await _context.Sesiones.FindAsync(sesionId);
        if (sesion is null)
            return NotFound();

        sesion.Revocar("Cierre de sesión por el usuario");
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
