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

        public async Task<UsuarioEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id);

            return entity;
        }
    }
}
