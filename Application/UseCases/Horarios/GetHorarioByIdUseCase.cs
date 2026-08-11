
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Horarios
{
    public class GetHorarioByIdUseCase
    {
        private readonly IGenericRepository<HorarioEntity> _repo;

        public GetHorarioByIdUseCase(IGenericRepository<HorarioEntity> repo)
        {
            _repo = repo;
        }

        public async Task<HorarioEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id);

            return entity;
        }
    }
}