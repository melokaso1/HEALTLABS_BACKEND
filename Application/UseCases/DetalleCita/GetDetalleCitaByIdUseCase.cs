using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.DetalleCita
{
    public class GetDetalleCitaByIdUseCase
    {
        private readonly IGenericRepository<DetalleCitaEntity> _repo;

        public GetDetalleCitaByIdUseCase(IGenericRepository<DetalleCitaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<DetalleCitaEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id);
            return entity;
        }
    }
}