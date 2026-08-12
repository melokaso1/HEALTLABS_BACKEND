using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PersonaDireccion
{
    public class DeletePersonaDireccionUseCase
    {
        private readonly IGenericRepository<PersonaDireccionEntity> _repo;

        public DeletePersonaDireccionUseCase(IGenericRepository<PersonaDireccionEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
