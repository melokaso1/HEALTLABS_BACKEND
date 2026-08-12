using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Permiso
{
    public class DeletePermisoUseCase
    {
        private readonly IGenericRepository<PermisoEntity> _repo;

        public DeletePermisoUseCase(IGenericRepository<PermisoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
