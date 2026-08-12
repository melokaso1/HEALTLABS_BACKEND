using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Antecedentes
{
    public class GetAntecedentesByFechaUseCase
    {
        private readonly IAntecedentesRepository _repo;

        public GetAntecedentesByFechaUseCase(IAntecedentesRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<AntecedenteEntity>> EntityAsync(DateOnly date)
        {
            return await _repo.GetEntityByDateAsync(date);
        }
    }
}
