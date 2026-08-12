using Application.DTOs.PersonaDireccion;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PersonaDireccion
{
    public class UpdatePersonaDireccionUseCase
    {
        private readonly IGenericRepository<PersonaDireccionEntity> _repo;

        public UpdatePersonaDireccionUseCase(IGenericRepository<PersonaDireccionEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdatePersonaDireccionDto dto)
        {
            var persona_direccion = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            persona_direccion.Update(dto.Direccion, dto.Ciudad, dto.Principal);
            await _repo.UpdateAsync(persona_direccion);
        }
    }
}
