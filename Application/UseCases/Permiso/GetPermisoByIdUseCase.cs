using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Permiso
{
    public class GetPermisoByIdUseCase
    {
        private readonly IGenericRepository<PermisoEntity> _repo;

        public GetPermisoByIdUseCase(IGenericRepository<PermisoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<PermisoEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El permiso no existe.");
            return entity;
        }
    }
}
