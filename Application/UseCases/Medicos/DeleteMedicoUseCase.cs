using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Medicos
{
    public class DeleteMedicoUseCase
    {
        private readonly IGenericRepository<MedicoEntity> _repo;
        public DeleteMedicoUseCase(IGenericRepository<MedicoEntity> repo)
        {
            _repo = repo;
        }
        public async Task ExecuteAsync(Guid id)
        {

            await _repo.DeleteAsync(id);
        }
    }
}