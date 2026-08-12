using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TratamientoPosologia
{
    public class DeleteTratamientoPosologiaUseCase
    {
        private readonly IGenericRepository<TratamientoPosologiaEntity> _repo;

        public DeleteTratamientoPosologiaUseCase(IGenericRepository<TratamientoPosologiaEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
