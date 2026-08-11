using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Pacientes
{
    public class GetAllPacienteUseCase
    {
        private readonly IGenericRepository<PacienteEntity> _repo;

        public GetAllPacienteUseCase(IGenericRepository<PacienteEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PacienteEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}