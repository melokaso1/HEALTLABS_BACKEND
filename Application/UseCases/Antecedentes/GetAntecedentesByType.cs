using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Antecedentes
{
    public class GetAntecedentesByType
    {
        private readonly IAntecedentesRepository<AntecedenteEntity> _repo;

        public GetAntecedentesByType(IAntecedentesRepository<AntecedenteEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<AntecedenteEntity>> EntityAsync(string type)
        {
            var entity = await _repo.GetEntityByType(type);

            return entity;

        }
    }
}
