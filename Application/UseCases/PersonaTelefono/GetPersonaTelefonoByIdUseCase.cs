using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PersonaTelefono
{
    public class GetPersonaTelefonoByIdUseCase
    {
        private readonly IGenericRepository<PersonaTelefonoEntity> _repo;

        public GetPersonaTelefonoByIdUseCase(IGenericRepository<PersonaTelefonoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<PersonaTelefonoEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El teléfono de la persona no existe.");
            return entity;
        }
    }
}
