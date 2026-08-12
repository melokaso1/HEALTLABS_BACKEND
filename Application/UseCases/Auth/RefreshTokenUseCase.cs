using Application.Common;
using Application.DTOs.Auth;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Auth;

public sealed class RefreshTokenUseCase
{
    private readonly IGenericRepository<SesionEntity> _sesiones;
    private readonly IGenericRepository<UsuarioEntity> _usuarios;
    private readonly IGenericRepository<RolEntity> _roles;
    private readonly IJwtTokenGenerator _jwt;

    public RefreshTokenUseCase(
        IGenericRepository<SesionEntity> sesiones,
        IGenericRepository<UsuarioEntity> usuarios,
        IGenericRepository<RolEntity> roles,
        IJwtTokenGenerator jwt)
    {
        _sesiones = sesiones;
        _usuarios = usuarios;
        _roles = roles;
        _jwt = jwt;
    }

    public async Task<UseCaseResult<LoginResponseDto>> ExecuteAsync(RefreshTokenRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
            return UseCaseResult<LoginResponseDto>.Fail("Refresh token requerido.");

        var ahora = DateTime.UtcNow;
        var hash = _jwt.HashToken(request.RefreshToken);
        var sesion = await _sesiones.FirstOrDefaultAsync(s => s.RefreshTokenHash == hash);

        if (sesion is null)
            return UseCaseResult<LoginResponseDto>.Fail("Refresh token inválido.", 401);

        if (sesion.RevocadoEn.HasValue)
            return UseCaseResult<LoginResponseDto>.Fail("La sesión fue revocada.", 401);

        if (sesion.RefreshExpiresAt < ahora)
            return UseCaseResult<LoginResponseDto>.Fail("Refresh token expirado.", 401);

        var usuario = await _usuarios.GetEntityByIdAsync(sesion.UsuarioId);
        if (usuario is null || !usuario.Activo)
            return UseCaseResult<LoginResponseDto>.Fail("Usuario no válido.", 401);

        var rol = await _roles.GetEntityByIdAsync(usuario.RolId);
        var rolNombre = rol?.NombreRol ?? "Sin Rol";

        sesion.Revocar("Rotación de refresh token");

        var jti = Guid.NewGuid().ToString("N");
        var refreshRaw = _jwt.GenerateRefreshToken();
        var accessExpires = ahora.AddMinutes(_jwt.AccessDurationMinutes);
        var refreshExpires = ahora.AddDays(_jwt.RefreshDurationInDays);

        var nuevaSesion = new SesionEntity(
            usuarioId: usuario.Id,
            jti: jti,
            refreshTokenHash: _jwt.HashToken(refreshRaw),
            accessExpiresAt: accessExpires,
            refreshExpiresAt: refreshExpires,
            emitidoEn: ahora,
            ultimoUso: ahora,
            revocadoEn: null,
            motivoRevocacion: null,
            ip: request.IpAddress ?? sesion.Ip,
            userAgent: request.UserAgent ?? sesion.UserAgent,
            dispositivo: sesion.Dispositivo);

        await _sesiones.UpdateAsync(sesion);
        await _sesiones.AddAsync(nuevaSesion);

        var accessToken = _jwt.GenerateAccessToken(usuario, rolNombre, jti);

        return UseCaseResult<LoginResponseDto>.Success(new LoginResponseDto
        {
            UsuarioId = usuario.Id,
            SesionId = nuevaSesion.Id,
            Username = usuario.Username,
            Email = usuario.Email,
            RolNombre = rolNombre,
            DebeCambiarPassword = usuario.DebeCambiarPassword,
            AccessExpiresAt = accessExpires,
            RefreshExpiresAt = refreshExpires,
            AccessToken = accessToken,
            RefreshToken = refreshRaw
        });
    }
}
