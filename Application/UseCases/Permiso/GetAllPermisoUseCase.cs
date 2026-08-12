using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Permiso
{
    public class GetAllPermisoUseCase
    {
        private readonly IGenericRepository<PermisoEntity> _repo;

        public GetAllPermisoUseCase(IGenericRepository<PermisoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PermisoEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}
