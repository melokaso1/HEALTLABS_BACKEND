using Application.DTOs.Usuario;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Usuarios
{
    public class SetUsuarioActivoUseCase
    {
        private readonly IGenericRepository<UsuarioEntity> _repo;

        public SetUsuarioActivoUseCase(IGenericRepository<UsuarioEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, SetUsuarioActivoDto dto)
        {
            var usuario = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El usuario solicitado no existe.");

            var intentosFallidos = usuario.IntentosFallidos;
            var bloqueadoHasta = usuario.BloqueadoHasta;

            if (dto.Activo)
            {
                intentosFallidos = 0;
                bloqueadoHasta = null;
            }

            var tokenVersion = usuario.TokenVersion;
            if (!dto.Activo && usuario.Activo)
                tokenVersion++;

            usuario.Update(
                usuario.RolId,
                usuario.Username,
                usuario.Email,
                usuario.PasswordHash,
                dto.Activo,
                usuario.UltimoLogin,
                intentosFallidos,
                bloqueadoHasta,
                usuario.DebeCambiarPassword,
                usuario.PasswordChangedAt,
                tokenVersion);

            try
            {
                await _repo.UpdateAsync(usuario);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException(
                    ex.InnerException?.Message ?? ex.Message,
                    ex);
            }
        }
    }
}
