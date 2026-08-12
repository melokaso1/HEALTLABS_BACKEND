using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Antecedentes
{
    public class GetAntecedenteByIdUseCase
    {
        private readonly IGenericRepository<AntecedenteEntity> _repo;

        public GetAntecedenteByIdUseCase(IGenericRepository<AntecedenteEntity> repo)
        {
            _repo = repo;
        }

        public async Task<AntecedenteEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id);

            return entity;
        }
    }
}
