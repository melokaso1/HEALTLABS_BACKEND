using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Especialidad
{
    public class DeleteEspecialidadUseCase
    {
        private readonly IGenericRepository<EspecialidadEntity> _repo;
        public DeleteEspecialidadUseCase(IGenericRepository<EspecialidadEntity> repo)
        {
            _repo = repo;
        }
        public async Task ExecuteAsync(Guid id)
        {

            await _repo.DeleteAsync(id);
        }
     }
}