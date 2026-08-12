using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PersonaTelefono
{
    public class DeletePersonaTelefonoUseCase
    {
        private readonly IGenericRepository<PersonaTelefonoEntity> _repo;

        public DeletePersonaTelefonoUseCase(IGenericRepository<PersonaTelefonoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
