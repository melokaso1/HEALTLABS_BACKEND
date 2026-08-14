using Application.DTOs.Usuario;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Usuarios
{
    public class GetUsuarioByIdUseCase
    {
        private readonly IGenericRepository<UsuarioEntity> _repo;

        public GetUsuarioByIdUseCase(IGenericRepository<UsuarioEntity> repo)
        {
            _repo = repo;
        }

        public async Task<UsuarioDto> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El usuario no existe.");
            return UsuarioDtoMapper.ToDto(entity);
        }
    }
}
