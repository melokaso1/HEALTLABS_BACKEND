using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.MedicoEspecialidad
{
    public class DeleteMedicoEspecialidadUseCase
    {
        private readonly IGenericRepository<MedicoEspecialidadEntity> _repo;

        public DeleteMedicoEspecialidadUseCase(IGenericRepository<MedicoEspecialidadEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
