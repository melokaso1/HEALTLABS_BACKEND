using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Pacientes
{
    public class GetPacienteByIdUseCase
    {
        private readonly IGenericRepository<PacienteEntity> _repo;

        public GetPacienteByIdUseCase(IGenericRepository<PacienteEntity> repo)
        {
            _repo = repo;
        }

        public async Task<PacienteEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El paciente no existe.");
            return entity;
        }
    }
}