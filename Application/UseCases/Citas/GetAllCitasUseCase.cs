using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Citas
{
    public class GetAllCitasUseCase
    {
        private readonly IGenericRepository<CitaEntity> _repo;

        public GetAllCitasUseCase(IGenericRepository<CitaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CitaEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}