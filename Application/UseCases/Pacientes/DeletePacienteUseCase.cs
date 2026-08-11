using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Pacientes
{
    public class DeletePacienteUseCase
    {
        private readonly IGenericRepository<PacienteEntity> _repo;
        public DeletePacienteUseCase(IGenericRepository<PacienteEntity> repo)
        {
            _repo = repo;
        }
        public async Task ExecuteAsync(Guid id)
        {

            await _repo.DeleteAsync(id);
        }
    }
}
