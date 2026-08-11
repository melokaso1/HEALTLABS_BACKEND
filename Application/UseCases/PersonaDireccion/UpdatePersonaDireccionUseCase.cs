using Application.DTOs.Paciente;
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
            var persona_direccion = await _repo.GetEntityByIdAsync(id);

            if (persona_direccion == null)
            {
                throw new ArgumentException("No Existe");
            }

            persona_direccion.PersonaId = dto.PersonaId;
            persona_direccion.Direccion = dto.Direccion;
            persona_direccion.Ciudad = dto.Ciudad;
            persona_direccion.Tipo = dto.Tipo;
            persona_direccion.Principal = dto.Principal;

            await _repo.UpdateAsync(persona_direccion);

        }
    }
}