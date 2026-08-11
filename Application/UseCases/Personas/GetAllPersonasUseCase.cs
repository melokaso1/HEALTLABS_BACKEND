using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Personas
{
    public class GetAllPersonasUseCase
    {
        private readonly IGenericRepository<PersonaEntity> _repo;

        public GetAllPersonasUseCase(IGenericRepository<PersonaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PersonaEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}