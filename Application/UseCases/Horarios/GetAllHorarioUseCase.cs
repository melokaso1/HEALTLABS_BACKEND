using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Horarios
{
    public class GetAllHorarioUseCase
    {
        private readonly IGenericRepository<HorarioEntity> _repo;

        public GetAllHorarioUseCase(IGenericRepository<HorarioEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<HorarioEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}