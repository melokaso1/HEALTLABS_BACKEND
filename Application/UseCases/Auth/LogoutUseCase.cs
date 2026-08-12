using Application.Common;
using Application.DTOs.Auth;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Auth;

public sealed class LogoutUseCase
{
    private readonly IGenericRepository<SesionEntity> _sesiones;

    public LogoutUseCase(IGenericRepository<SesionEntity> sesiones)
    {
        _sesiones = sesiones;
    }

    public async Task<UseCaseResult> ExecuteAsync(LogoutRequestDto request)
    {
        SesionEntity? sesion = null;

        if (request.SesionId.HasValue)
            sesion = await _sesiones.GetEntityByIdAsync(request.SesionId.Value);
        else if (!string.IsNullOrWhiteSpace(request.Jti))
            sesion = await _sesiones.FirstOrDefaultAsync(s => s.Jti == request.Jti);

        if (sesion is null)
            return UseCaseResult.Fail("Sesión no encontrada.", 404);

        if (sesion.RevocadoEn.HasValue)
            return UseCaseResult.Success();

        sesion.Revocar("Cierre de sesión por el usuario");
        await _sesiones.UpdateAsync(sesion);
        return UseCaseResult.Success();
    }
}
