using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Roles
{
    public class DeleteRolUseCase
    {
        private readonly IGenericRepository<RolEntity> _repo;

        public DeleteRolUseCase(IGenericRepository<RolEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
