using Application.DTOs.Cita;
using Application.DTOs.Persona;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.UseCases.Personas
{
    public class UpdatePersonaUseCase
    {
        private readonly IGenericRepository<PersonaEntity> _repo;
        private readonly IGenericRepository<SexoEntity> _sexoRepo;

        public UpdatePersonaUseCase(
            IGenericRepository<PersonaEntity> repo,
            IGenericRepository<SexoEntity> sexoRepo)
        {
            _repo = repo;
            _sexoRepo = sexoRepo;
        }

        public async Task ExecuteAsync(Guid id, UpdatePersonaDto dto)
        {
            var persona = await _repo.GetEntityByIdAsync(id);

            if (persona == null)
            {
                throw new KeyNotFoundException("No Existe");
            }

            if (dto.SexoId.HasValue && !await _sexoRepo.AnyAsync(sexo => sexo.Id == dto.SexoId.Value))
                throw new InvalidOperationException("El sexo especificado no existe.");

            persona.Nombre = dto.Nombre;
            persona.Apellido = dto.Apellido;
            persona.TipoDocumentoId = dto.TipoDocumentoId;
            persona.NumeroDocumento = NumeroDocumentoValueObject.Create(dto.NumeroDocumento ?? string.Empty).Value;
            persona.FechaNacimiento = dto.FechaNacimiento;
            persona.SexoId = dto.SexoId;

            await _repo.UpdateAsync(persona);

        }
    }
}