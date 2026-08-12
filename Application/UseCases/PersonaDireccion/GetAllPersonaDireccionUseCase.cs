using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PersonaDireccion
{
    public class GetAllPersonaDireccionUseCase
    {
        private readonly IGenericRepository<PersonaDireccionEntity> _repo;

        public GetAllPersonaDireccionUseCase(IGenericRepository<PersonaDireccionEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PersonaDireccionEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}
