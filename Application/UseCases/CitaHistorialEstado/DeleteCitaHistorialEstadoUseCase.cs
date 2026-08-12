using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.CitaHistorialEstado
{
    public class DeleteCitaHistorialEstadoUseCase
    {
        private readonly IGenericRepository<CitaHistorialEstadoEntity> _repo;

        public DeleteCitaHistorialEstadoUseCase(IGenericRepository<CitaHistorialEstadoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
