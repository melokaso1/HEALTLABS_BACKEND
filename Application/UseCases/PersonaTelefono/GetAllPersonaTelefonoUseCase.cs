using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PersonaTelefono
{
    public class GetAllPersonaTelefonoUseCase
    {
        private readonly IGenericRepository<PersonaTelefonoEntity> _repo;

        public GetAllPersonaTelefonoUseCase(IGenericRepository<PersonaTelefonoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PersonaTelefonoEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}
