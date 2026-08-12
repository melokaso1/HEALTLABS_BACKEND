using Application.DTOs.Paciente;
using Application.DTOs.PersonaDireccion;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PersonaDireccion
{
    public class CreatePersonaDireccionUseCase
    {
        private readonly IGenericRepository<PersonaDireccionEntity> _repo;

        public CreatePersonaDireccionUseCase(IGenericRepository<PersonaDireccionEntity> repo)
        {
            _repo = repo;
        }

        public async Task<PersonaDireccionEntity> ExecuteAsync(CreatePersonaDireccionDto dto)
        {
            var persona_direccion = new PersonaDireccionEntity(
                                        dto.PersonaId,
                                        dto.Direccion,
                                        dto.Ciudad,
                                        dto.Principal
                                        );

            return await _repo.AddAsync(persona_direccion);
        }
    }
}

