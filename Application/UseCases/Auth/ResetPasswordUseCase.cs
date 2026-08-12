using Application.Common;
using Application.DTOs.Auth;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.UseCases.Auth;

public sealed class ResetPasswordUseCase
{
    private readonly IGenericRepository<TokenRecuperacionEntity> _tokens;
    private readonly IGenericRepository<UsuarioEntity> _usuarios;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwt;

    public ResetPasswordUseCase(
        IGenericRepository<TokenRecuperacionEntity> tokens,
        IGenericRepository<UsuarioEntity> usuarios,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwt)
    {
        _tokens = tokens;
        _usuarios = usuarios;
        _passwordHasher = passwordHasher;
        _jwt = jwt;
    }

    public async Task<UseCaseResult<object>> ExecuteAsync(ResetPasswordDto request)
    {
        var token = await _tokens.GetEntityByIdAsync(request.TokenId);
        if (token is null || token.TokenHash != _jwt.HashToken(request.TokenRaw))
            return UseCaseResult<object>.Fail("El token de recuperación es inválido.");

        if (token.UsadoEn.HasValue)
            return UseCaseResult<object>.Fail("El token de recuperación ya ha sido utilizado.");

        if (token.ExpiresAt < DateTime.UtcNow)
            return UseCaseResult<object>.Fail("El token de recuperación ha expirado.");

        var usuario = await _usuarios.GetEntityByIdAsync(token.UsuarioId);
        if (usuario is null)
            return UseCaseResult<object>.Fail("Usuario no encontrado.");

        PasswordValueObject.Create(request.NewPassword);

        var ahora = DateTime.UtcNow;
        token.Update(
            usuarioId: token.UsuarioId,
            tokenHash: token.TokenHash,
            expiresAt: token.ExpiresAt,
            usadoEn: ahora,
            ipSolicitud: token.IpSolicitud);

        usuario.Update(
            rolId: usuario.RolId,
            username: usuario.Username,
            email: usuario.Email,
            passwordHash: _passwordHasher.Hash(request.NewPassword),
            activo: usuario.Activo,
            ultimoLogin: usuario.UltimoLogin,
            intentosFallidos: 0,
            bloqueadoHasta: null,
            debeCambiarPassword: false,
            passwordChangedAt: ahora,
            tokenVersion: usuario.TokenVersion + 1);

        await _tokens.UpdateAsync(token);
        await _usuarios.UpdateAsync(usuario);

        return UseCaseResult<object>.Success(new { mensaje = "La contraseña ha sido actualizada correctamente." });
    }
}
