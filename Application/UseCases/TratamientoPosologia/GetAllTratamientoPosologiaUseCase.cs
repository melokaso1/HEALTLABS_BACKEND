using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TratamientoPosologia
{
    public class GetAllTratamientoPosologiaUseCase
    {
        private readonly IGenericRepository<TratamientoPosologiaEntity> _repo;

        public GetAllTratamientoPosologiaUseCase(IGenericRepository<TratamientoPosologiaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TratamientoPosologiaEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}
