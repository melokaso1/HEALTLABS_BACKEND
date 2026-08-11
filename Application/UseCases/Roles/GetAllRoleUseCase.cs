using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Roles
{
    public class GetAllRoleUseCase
    {
        private readonly IGenericRepository<RolEntity> _repo;

        public GetAllRoleUseCase(IGenericRepository<RolEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RolEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}