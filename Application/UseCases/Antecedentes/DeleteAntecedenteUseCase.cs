using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Antecedentes
{
    public class DeleteAntecedenteUseCase
    {
        private readonly IGenericRepository<AntecedenteEntity> _repo;

        public DeleteAntecedenteUseCase(IGenericRepository<AntecedenteEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
