using Application.DTOs.PersonaTelefono;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PersonaTelefono
{
    public class CreatePersonaTelefonoUseCase
    {
        private readonly IGenericRepository<PersonaTelefonoEntity> _repo;

        public CreatePersonaTelefonoUseCase(IGenericRepository<PersonaTelefonoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<PersonaTelefonoEntity> ExecuteAsync(CreatePersonaTelefonoDto dto)
        {
            var entity = new PersonaTelefonoEntity(dto.PersonaId, dto.Telefono, dto.Tipo, dto.Principal);
            return await _repo.AddAsync(entity);
        }
    }
}
