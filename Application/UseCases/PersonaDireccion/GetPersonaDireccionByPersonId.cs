using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PersonaDireccion
{
    public class GetPersonaDireccionByPersonId
    {
        private readonly IPersonaDireccionRepository<PersonaDireccionEntity> _repo;

        public GetPersonaDireccionByPersonId(IPersonaDireccionRepository<PersonaDireccionEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PersonaDireccionEntity>> ExecuteAsync(Guid PersonId)
        {
            var entity = await _repo.GetEntityByPersonIdAsync(PersonId);

            return entity;
        }
    }
}