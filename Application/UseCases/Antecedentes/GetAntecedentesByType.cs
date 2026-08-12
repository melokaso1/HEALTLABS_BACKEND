using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Antecedentes
{
    public class GetAntecedentesByType
    {
        private readonly IAntecedentesRepository _repo;

        public GetAntecedentesByType(IAntecedentesRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<AntecedenteEntity>> EntityAsync(string type)
        {
            return await _repo.GetEntityByType(type);
        }
    }
}
