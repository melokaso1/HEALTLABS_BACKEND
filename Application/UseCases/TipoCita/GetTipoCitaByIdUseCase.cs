using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TipoCita
{
    public class GetTipoCitaByIdUseCase
    {
        private readonly IGenericRepository<TipoCitaEntity> _repo;

        public GetTipoCitaByIdUseCase(IGenericRepository<TipoCitaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<TipoCitaEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El tipo de cita no existe.");
            return entity;
        }
    }
}
