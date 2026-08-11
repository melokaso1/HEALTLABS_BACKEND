using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Horarios
{
    public class DeleteHorarioUseCase
    {
        private readonly IGenericRepository<HorarioEntity> _repo;
        public DeleteHorarioUseCase(IGenericRepository<HorarioEntity> repo)
        {
            _repo = repo;
        }
        public async Task ExecuteAsync(Guid id)
        {

            await _repo.DeleteAsync(id);
        }
    }
}