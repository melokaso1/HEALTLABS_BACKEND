using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.RolPermiso
{
    public class GetAllRolPermisoUseCase
    {
        private readonly IGenericRepository<RolPermisoEntity> _repo;

        public GetAllRolPermisoUseCase(IGenericRepository<RolPermisoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RolPermisoEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}
