using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.DetalleDiagnostico
{
    public class GetAllDetalleDiagnosticoUseCase
    {
        private readonly IGenericRepository<DetalleDiagnosticoEntity> _repo;

        public GetAllDetalleDiagnosticoUseCase(IGenericRepository<DetalleDiagnosticoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<DetalleDiagnosticoEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}
