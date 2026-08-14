using Application.DTOs.Usuario;
using Domain.Entities;

namespace Application.UseCases.Usuarios;

internal static class UsuarioDtoMapper
{
    public static UsuarioDto ToDto(UsuarioEntity usuario) => new()
    {
        UsuarioId = usuario.Id,
        EmpleadoId = usuario.EmpleadoId,
        RolId = usuario.RolId,
        Username = usuario.Username,
        Email = usuario.Email,
        Activo = usuario.Activo,
        FechaCreacion = usuario.FechaCreacion,
        UltimoLogin = usuario.UltimoLogin,
        DebeCambiarPassword = usuario.DebeCambiarPassword,
        TokenVersion = usuario.TokenVersion,
        NombreCompleto = usuario.Empleado?.Persona is { } persona
            ? $"{persona.Nombre} {persona.Apellido}".Trim()
            : null,
        RolNombre = usuario.Rol?.NombreRol
    };
}
