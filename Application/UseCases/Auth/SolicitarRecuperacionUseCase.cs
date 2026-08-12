using Application.Common;
using Application.DTOs.Auth;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Auth;

public sealed class SolicitarRecuperacionUseCase
{
    private readonly IGenericRepository<UsuarioEntity> _usuarios;
    private readonly IGenericRepository<TokenRecuperacionEntity> _tokens;
    private readonly IJwtTokenGenerator _jwt;

    public SolicitarRecuperacionUseCase(
        IGenericRepository<UsuarioEntity> usuarios,
        IGenericRepository<TokenRecuperacionEntity> tokens,
        IJwtTokenGenerator jwt)
    {
        _usuarios = usuarios;
        _tokens = tokens;
        _jwt = jwt;
    }

    public async Task<UseCaseResult<object>> ExecuteAsync(SolicitarRecuperacionDto request)
    {
        var mensajeGenerico = new
        {
            mensaje = "Si la cuenta existe en el sistema, se ha generado el token de recuperación."
        };

        var usuario = await _usuarios.FirstOrDefaultAsync(u =>
            u.Email == request.EmailOrUsername || u.Username == request.EmailOrUsername);

        if (usuario is null)
            return UseCaseResult<object>.Success(mensajeGenerico);

        var tokenRaw = Guid.NewGuid().ToString("N");
        var tokenEntity = new TokenRecuperacionEntity(
            usuarioId: usuario.Id,
            tokenHash: _jwt.HashToken(tokenRaw),
            expiresAt: DateTime.UtcNow.AddHours(24),
            usadoEn: null,
            ipSolicitud: request.IpAddress);

        await _tokens.AddAsync(tokenEntity);

        // En desarrollo se expone el token; en producción iría por email.
        return UseCaseResult<object>.Success(new
        {
            mensaje = "Token de recuperación generado exitosamente.",
            tokenId = tokenEntity.Id,
            tokenRaw,
            expiresAt = tokenEntity.ExpiresAt
        });
    }
}
