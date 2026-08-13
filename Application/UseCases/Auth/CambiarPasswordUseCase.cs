using Application.Common;
using Application.DTOs.Auth;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.UseCases.Auth;

public sealed class CambiarPasswordUseCase
{
    private readonly IGenericRepository<UsuarioEntity> _usuarios;
    private readonly IPasswordHasher _passwordHasher;

    public CambiarPasswordUseCase(
        IGenericRepository<UsuarioEntity> usuarios,
        IPasswordHasher passwordHasher)
    {
        _usuarios = usuarios;
        _passwordHasher = passwordHasher;
    }

    public async Task<UseCaseResult<object>> ExecuteAsync(string identifier, CambiarPasswordDto request)
    {
        if (string.IsNullOrWhiteSpace(request.CurrentPassword))
            return UseCaseResult<object>.Fail("Debe ingresar su contraseña actual.", 400);

        if (string.IsNullOrWhiteSpace(request.NewPassword) || request.NewPassword.Length < 8)
            return UseCaseResult<object>.Fail("La nueva contraseña debe tener al menos 8 caracteres.", 400);

        UsuarioEntity? usuario = null;

        if (Guid.TryParse(identifier, out var idGuid))
        {
            usuario = await _usuarios.GetEntityByIdAsync(idGuid);
        }

        if (usuario is null)
        {
            usuario = await _usuarios.FirstOrDefaultAsync(u => u.Username == identifier || u.Email == identifier);
        }

        if (usuario is null)
            return UseCaseResult<object>.Fail("Usuario no encontrado.", 404);

        if (!_passwordHasher.Verify(request.CurrentPassword, usuario.PasswordHash))
            return UseCaseResult<object>.Fail("La contraseña actual es incorrecta.", 400);

        try
        {
            PasswordValueObject.Create(request.NewPassword);
        }
        catch (InvalidOperationException ex)
        {
            return UseCaseResult<object>.Fail(ex.Message, 400);
        }

        var ahora = DateTime.UtcNow;
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

        await _usuarios.UpdateAsync(usuario);

        return UseCaseResult<object>.Success(new { mensaje = "La contraseña ha sido actualizada correctamente en el servidor." });
    }
}
