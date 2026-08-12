using Application.DTOs.PersonaTelefono;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PersonaTelefono
{
    public class UpdatePersonaTelefonoUseCase
    {
        private readonly IGenericRepository<PersonaTelefonoEntity> _repo;

        public UpdatePersonaTelefonoUseCase(IGenericRepository<PersonaTelefonoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdatePersonaTelefonoDto dto)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            entity.Update(dto.PersonaId, dto.Telefono, dto.Tipo, dto.Principal);
            await _repo.UpdateAsync(entity);
        }
    }
}
