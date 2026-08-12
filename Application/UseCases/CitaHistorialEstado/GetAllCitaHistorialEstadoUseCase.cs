using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.CitaHistorialEstado
{
    public class GetAllCitaHistorialEstadoUseCase
    {
        private readonly IGenericRepository<CitaHistorialEstadoEntity> _repo;

        public GetAllCitaHistorialEstadoUseCase(IGenericRepository<CitaHistorialEstadoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CitaHistorialEstadoEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}
