using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PacienteAlergia
{
    public class DeletePacienteAlergiaUseCase
    {
        private readonly IGenericRepository<PacienteAlergiaEntity> _repo;

        public DeletePacienteAlergiaUseCase(IGenericRepository<PacienteAlergiaEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
