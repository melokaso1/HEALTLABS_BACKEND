using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TipoCita
{
    public class DeleteTipoCitaUseCase
    {
        private readonly IGenericRepository<TipoCitaEntity> _repo;

        public DeleteTipoCitaUseCase(IGenericRepository<TipoCitaEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
