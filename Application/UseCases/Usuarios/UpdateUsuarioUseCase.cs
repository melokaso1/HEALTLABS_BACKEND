using Application.DTOs.Tratamiento;
using Application.DTOs.Usuario;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Usuarios
{
    public class UpdateUsuarioUseCase
    {
        private readonly IGenericRepository<UsuarioEntity> _repo;

        public UpdateUsuarioUseCase(IGenericRepository<UsuarioEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateUsuarioDto dto)
        {
            var usuario = await _repo.GetEntityByIdAsync(id);

            if (usuario == null)
            {
                throw new ArgumentException("No Existe");
            }

            usuario.EmpleadoId = dto.EmpleadoId;
            usuario.RolId = dto.RolId;
            usuario.Username = dto.Username;
            usuario.Email = dto.Email;
            /*usuario.Password = dto.Password;*/
            usuario.Activo = dto.Activo;
            usuario.DebeCambiarPassword = dto.DebeCambiarPassword;

            await _repo.UpdateAsync(usuario);

        }
    }
}