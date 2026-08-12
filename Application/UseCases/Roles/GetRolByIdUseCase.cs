using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Roles
{
    public class GetRolByIdUseCase
    {
        private readonly IGenericRepository<RolEntity> _repo;

        public GetRolByIdUseCase(IGenericRepository<RolEntity> repo)
        {
            _repo = repo;
        }

        public async Task<RolEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El rol no existe.");
            return entity;
        }
    }
}
