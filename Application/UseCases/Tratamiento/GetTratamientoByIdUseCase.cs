using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Tratamiento
{
    public class GetTratamientoByIdUseCase
    {
        private readonly IGenericRepository<TratamientoEntity> _repo;

        public GetTratamientoByIdUseCase(IGenericRepository<TratamientoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<TratamientoEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id);

            return entity;
        }
    }
}