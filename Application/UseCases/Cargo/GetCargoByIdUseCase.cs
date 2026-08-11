using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Cargo
{
    public class GetCargoByIdUseCase
    {
        private readonly IGenericRepository<CargoEntity> _repo;

        public GetCargoByIdUseCase(IGenericRepository<CargoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<CargoEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id);
            return entity;
        }
    }
}