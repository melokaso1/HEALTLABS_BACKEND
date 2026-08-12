using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.CitaHistorialEstado
{
    public class GetCitaHistorialEstadoByIdUseCase
    {
        private readonly IGenericRepository<CitaHistorialEstadoEntity> _repo;

        public GetCitaHistorialEstadoByIdUseCase(IGenericRepository<CitaHistorialEstadoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<CitaHistorialEstadoEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El historial de estado de la cita no existe.");
            return entity;
        }
    }
}
