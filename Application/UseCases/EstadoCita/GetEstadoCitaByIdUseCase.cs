using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.EstadoCita
{
    public class GetEstadoCitaByIdUseCase
    {
        private readonly IGenericRepository<EstadoCitaEntity> _repo;

        public GetEstadoCitaByIdUseCase(IGenericRepository<EstadoCitaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<EstadoCitaEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id);

            return entity;
        }
    }
}