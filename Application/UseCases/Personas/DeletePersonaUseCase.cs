using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Personas
{
    public class DeletePersonaUseCase
    {
        private readonly IGenericRepository<PersonaEntity> _repo;
        public DeletePersonaUseCase(IGenericRepository<PersonaEntity> repo)
        {
            _repo = repo;
        }
        public async Task ExecuteAsync(Guid id)
        {

            await _repo.DeleteAsync(id);
        }
    }
}