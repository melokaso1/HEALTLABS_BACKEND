using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PersonaDireccion
{
    public class GetPersonaDireccionByIdUseCase
    {
        private readonly IGenericRepository<PersonaDireccionEntity> _repo;

        public GetPersonaDireccionByIdUseCase(IGenericRepository<PersonaDireccionEntity> repo)
        {
            _repo = repo;
        }

        public async Task<PersonaDireccionEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id);

            return entity;
        }
    }
}
