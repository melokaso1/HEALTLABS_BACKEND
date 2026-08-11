using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.DetalleDiagnostico
{
    public class GetDetalleDiagnosticoByIdUseCase
    {
        private readonly IGenericRepository<DetalleDiagnosticoEntity> _repo;

        public GetDetalleDiagnosticoByIdUseCase(IGenericRepository<DetalleDiagnosticoEntity> repo)
        {
            _repo = repo;
        }
        public async Task<DetalleDiagnosticoEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id);
            return entity;
        }
    }
}
