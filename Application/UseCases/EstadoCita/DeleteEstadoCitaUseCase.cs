using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.EstadoCita
{
    public class DeleteEstadoCitaUseCase
    {
        private readonly IGenericRepository<EstadoCitaEntity> _repo;

        public DeleteEstadoCitaUseCase(IGenericRepository<EstadoCitaEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
