using Application.DTOs.Cita;
using Application.DTOs.Persona;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Personas
{
    public class UpdatePersonaUseCase
    {
        private readonly IGenericRepository<PersonaEntity> _repo;

        public UpdatePersonaUseCase(IGenericRepository<PersonaEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdatePersonaDto dto)
        {
            var persona = await _repo.GetEntityByIdAsync(id);

            if (persona == null)
            {
                throw new KeyNotFoundException("No Existe");
            }

            persona.Nombre = dto.Nombre;
            persona.Apellido = dto.Apellido;
            persona.TipoDocumentoId = dto.TipoDocumentoId;
            persona.NumeroDocumento = dto.NumeroDocumento;
            persona.FechaNacimiento = dto.FechaNacimiento;
            persona.SexoId = dto.SexoId;

            await _repo.UpdateAsync(persona);

        }
    }
}