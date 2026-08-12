using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Sexo
{
    public class DeleteSexoUseCase
    {
        private readonly IGenericRepository<SexoEntity> _repo;

        public DeleteSexoUseCase(IGenericRepository<SexoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
