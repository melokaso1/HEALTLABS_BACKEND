using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Tratamiento
{
    public class GetAllTratamientoUseCase
    {
        private readonly IGenericRepository<TratamientoEntity> _repo;

        public GetAllTratamientoUseCase(IGenericRepository<TratamientoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TratamientoEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}
