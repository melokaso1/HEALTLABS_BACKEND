using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Tratamiento
{
    public class DeleteTratamientoUseCase
    {
        private readonly IGenericRepository<TratamientoEntity> _repo;
        public DeleteTratamientoUseCase(IGenericRepository<TratamientoEntity> repo)
        {
            _repo = repo;
        }
        public async Task ExecuteAsync(Guid id)
        {

            await _repo.DeleteAsync(id);
        }
    }
}