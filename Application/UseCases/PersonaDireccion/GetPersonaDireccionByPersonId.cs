using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PersonaDireccion
{
    public class GetPersonaDireccionByPersonId
    {
        private readonly IPersonaDireccionRepository _repo;

        public GetPersonaDireccionByPersonId(IPersonaDireccionRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PersonaDireccionEntity>> ExecuteAsync(Guid personId)
        {
            return await _repo.GetEntityByPersonIdAsync(personId);
        }
    }
}
