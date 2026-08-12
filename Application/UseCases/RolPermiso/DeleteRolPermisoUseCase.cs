using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.RolPermiso
{
    public class DeleteRolPermisoUseCase
    {
        private readonly IGenericRepository<RolPermisoEntity> _repo;

        public DeleteRolPermisoUseCase(IGenericRepository<RolPermisoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
