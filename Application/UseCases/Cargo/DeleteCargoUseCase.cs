using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Cargo
{
    public class DeleteCargoUseCase
    {
        private readonly IGenericRepository<CargoEntity> _repo;

        public DeleteCargoUseCase(IGenericRepository<CargoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
