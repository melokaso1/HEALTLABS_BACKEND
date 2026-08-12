using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Antecedentes
{
    public class GetAllAntecedenteUseCase
    {
        private readonly IGenericRepository<AntecedenteEntity> _repo;

        public GetAllAntecedenteUseCase(IGenericRepository<AntecedenteEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<AntecedenteEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}
