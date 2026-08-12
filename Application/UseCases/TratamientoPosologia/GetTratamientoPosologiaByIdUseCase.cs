using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TratamientoPosologia
{
    public class GetTratamientoPosologiaByIdUseCase
    {
        private readonly IGenericRepository<TratamientoPosologiaEntity> _repo;

        public GetTratamientoPosologiaByIdUseCase(IGenericRepository<TratamientoPosologiaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<TratamientoPosologiaEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El detalle de la posología no existe.");
            return entity;
        }
    }
}
