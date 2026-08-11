using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Diagnostico
{
    public class GetAllDiagnosticosUseCase
    {
        private readonly IGenericRepository<DiagnosticoEntity> _repo;

        public GetAllDiagnosticosUseCase(IGenericRepository<DiagnosticoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<DiagnosticoEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}