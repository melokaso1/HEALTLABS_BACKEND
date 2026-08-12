using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.DetalleDiagnostico
{
    public class DeleteDetalleDiagnosticoUseCase
    {
        private readonly IGenericRepository<DetalleDiagnosticoEntity> _repo;

        public DeleteDetalleDiagnosticoUseCase(IGenericRepository<DetalleDiagnosticoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
