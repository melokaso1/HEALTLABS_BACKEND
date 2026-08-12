using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.AtencionTratamiento
{
    public class DeleteAtencionTratamientoUseCase
    {
        private readonly IGenericRepository<AtencionTratamientoEntity> _repo;

        public DeleteAtencionTratamientoUseCase(IGenericRepository<AtencionTratamientoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
