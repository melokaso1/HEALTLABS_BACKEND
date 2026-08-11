using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Cargo
{
    public class GetAllCargoUseCase
    {
        private readonly IGenericRepository<CargoEntity> _repo;

        public GetAllCargoUseCase(IGenericRepository<CargoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CargoEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}