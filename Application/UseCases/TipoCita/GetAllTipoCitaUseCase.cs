using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TipoCita
{
    public class GetAllTipoCitaUseCase
    {
        private readonly IGenericRepository<TipoCitaEntity> _repo;

        public GetAllTipoCitaUseCase(IGenericRepository<TipoCitaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TipoCitaEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}
