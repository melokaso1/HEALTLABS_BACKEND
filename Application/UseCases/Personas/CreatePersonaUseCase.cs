using Application.DTOs.Cita;
using Application.DTOs.Persona;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Personas
{
    public class CreatePersonaUseCase
    {
        private readonly IGenericRepository<PersonaEntity> _repo;

        public CreatePersonaUseCase(IGenericRepository<PersonaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<PersonaEntity> ExecuteAsync(CreatePersonaDto dto)
        {
            var persona = new PersonaEntity(
                                        dto.Nombre,
                                        dto.Apellido,
                                        dto.TipoDocumentoId,
                                        dto.NumeroDocumento,
                                        dto.FechaNacimiento,
                                        dto.SexoId
                                        );

            return await _repo.AddAsync(persona);
        }
    }
}


