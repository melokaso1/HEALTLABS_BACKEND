using Application.DTOs.Cita;
using Application.DTOs.Persona;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.UseCases.Personas
{
    public class CreatePersonaUseCase
    {
        private readonly IGenericRepository<PersonaEntity> _repo;
        private readonly IGenericRepository<SexoEntity> _sexoRepo;

        public CreatePersonaUseCase(
            IGenericRepository<PersonaEntity> repo,
            IGenericRepository<SexoEntity> sexoRepo)
        {
            _repo = repo;
            _sexoRepo = sexoRepo;
        }

        public async Task<PersonaEntity> ExecuteAsync(CreatePersonaDto dto)
        {
            if (dto.SexoId.HasValue && !await _sexoRepo.AnyAsync(sexo => sexo.Id == dto.SexoId.Value))
                throw new InvalidOperationException("El sexo especificado no existe.");

            var persona = new PersonaEntity(
                                        dto.Nombre,
                                        dto.Apellido,
                                        dto.TipoDocumentoId,
                                        NumeroDocumentoValueObject.Create(dto.NumeroDocumento).Value,
                                        dto.FechaNacimiento,
                                        dto.SexoId
                                        );

            return await _repo.AddAsync(persona);
        }
    }
}


