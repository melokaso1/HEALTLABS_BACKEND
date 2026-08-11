using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Roles
{
    public class GetRoleByIdUseCase
    {
        private readonly IGenericRepository<RolEntity> _repo;

        public GetRoleByIdUseCase(IGenericRepository<RolEntity> repo)
        {
            _repo = repo;
        }

        public async Task<RolEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id);

            return entity;
        }
    }
}