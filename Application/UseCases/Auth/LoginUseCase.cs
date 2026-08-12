using Application.Common;
using Application.DTOs.Auth;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Auth;

public sealed class LoginUseCase
{
    private const int MaxFailedAttempts = 5;
    private static readonly TimeSpan LockoutDuration = TimeSpan.FromMinutes(15);

    private readonly IGenericRepository<UsuarioEntity> _usuarios;
    private readonly IGenericRepository<RolEntity> _roles;
    private readonly IGenericRepository<SesionEntity> _sesiones;
    private readonly IGenericRepository<LoginIntentoEntity> _loginIntentos;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwt;

    public LoginUseCase(
        IGenericRepository<UsuarioEntity> usuarios,
        IGenericRepository<RolEntity> roles,
        IGenericRepository<SesionEntity> sesiones,
        IGenericRepository<LoginIntentoEntity> loginIntentos,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwt)
    {
        _usuarios = usuarios;
        _roles = roles;
        _sesiones = sesiones;
        _loginIntentos = loginIntentos;
        _passwordHasher = passwordHasher;
        _jwt = jwt;
    }

    public async Task<UseCaseResult<LoginResponseDto>> ExecuteAsync(LoginRequestDto request)
    {
        var ahora = DateTime.UtcNow;
        var usuario = await _usuarios.FirstOrDefaultAsync(u =>
            u.Username == request.UsernameOrEmail || u.Email == request.UsernameOrEmail);

        if (usuario is null)
        {
            await RegistrarIntentoAsync(null, request, false, "Usuario no encontrado", ahora);
            return UseCaseResult<LoginResponseDto>.Fail("Credenciales inválidas.");
        }

        if (!usuario.Activo)
        {
            await RegistrarIntentoAsync(usuario.Id, request, false, "Usuario inactivo", ahora);
            return UseCaseResult<LoginResponseDto>.Fail("La cuenta de usuario se encuentra inactiva.");
        }

        if (usuario.BloqueadoHasta.HasValue && usuario.BloqueadoHasta > ahora)
        {
            await RegistrarIntentoAsync(usuario.Id, request, false, "Cuenta bloqueada", ahora);
            return UseCaseResult<LoginResponseDto>.Fail("La cuenta está temporalmente bloqueada. Intente más tarde.");
        }

        if (!VerifyAndUpgradePassword(usuario, request.Password))
        {
            usuario.IntentosFallidos += 1;
            if (usuario.IntentosFallidos >= MaxFailedAttempts)
            {
                usuario.BloqueadoHasta = ahora.Add(LockoutDuration);
                usuario.IntentosFallidos = 0;
            }

            await _usuarios.UpdateAsync(usuario);
            await RegistrarIntentoAsync(usuario.Id, request, false, "Contraseña incorrecta", ahora);
            return UseCaseResult<LoginResponseDto>.Fail("Credenciales inválidas.");
        }

        var rol = await _roles.GetEntityByIdAsync(usuario.RolId);
        var rolNombre = rol?.NombreRol ?? "Sin Rol";

        usuario.IntentosFallidos = 0;
        usuario.BloqueadoHasta = null;
        usuario.UltimoLogin = ahora;
        await _usuarios.UpdateAsync(usuario);

        await RegistrarIntentoAsync(usuario.Id, request, true, null, ahora);

        var jti = Guid.NewGuid().ToString("N");
        var refreshRaw = _jwt.GenerateRefreshToken();
        var accessExpires = ahora.AddMinutes(_jwt.AccessDurationMinutes);
        var refreshExpires = ahora.AddDays(_jwt.RefreshDurationInDays);

        var sesion = new SesionEntity(
            usuarioId: usuario.Id,
            jti: jti,
            refreshTokenHash: _jwt.HashToken(refreshRaw),
            accessExpiresAt: accessExpires,
            refreshExpiresAt: refreshExpires,
            emitidoEn: ahora,
            ultimoUso: ahora,
            revocadoEn: null,
            motivoRevocacion: null,
            ip: null,
            userAgent: null,
            dispositivo: null);

        await _sesiones.AddAsync(sesion);

        var accessToken = _jwt.GenerateAccessToken(usuario, rolNombre, jti);

        return UseCaseResult<LoginResponseDto>.Success(new LoginResponseDto
        {
            UsuarioId = usuario.Id,
            SesionId = sesion.Id,
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

    private bool VerifyAndUpgradePassword(UsuarioEntity usuario, string password)
    {
        if (_passwordHasher.Verify(password, usuario.PasswordHash))
            return true;

        // Compatibilidad con seeds/datos legacy en texto plano.
        if (usuario.PasswordHash == password)
        {
            usuario.PasswordHash = _passwordHasher.Hash(password);
            return true;
        }

        return false;
    }

    private async Task RegistrarIntentoAsync(
        Guid? usuarioId,
        LoginRequestDto request,
        bool exitoso,
        string? motivo,
        DateTime fecha)
    {
        await _loginIntentos.AddAsync(new LoginIntentoEntity(
            usuarioId,
            request.UsernameOrEmail,
            exitoso,
            motivo,
            null,
            null,
            fecha));
    }
}
