using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Personas
{
    public class GetByIdPersonaUseCase
    {
        private readonly IGenericRepository<PersonaEntity> _repo;

        public GetByIdPersonaUseCase(IGenericRepository<PersonaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<PersonaEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id);

            return entity;
        }
    }
}