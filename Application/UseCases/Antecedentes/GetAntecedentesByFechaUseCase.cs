using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Antecedentes
{
    public class GetAntecedentesByFechaUseCase
    {
        private readonly IAntecedentesRepository<AntecedenteEntity> _repo;

        public GetAntecedentesByFechaUseCase(IAntecedentesRepository<AntecedenteEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<AntecedenteEntity>> EntityAsync(DateOnly date)
        {
            var entity = await _repo.GetEntityByDateAsync(date);

            return entity;




        }
    }
}