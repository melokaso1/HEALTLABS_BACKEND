using Application.DTOs.CitaHistorialEstado;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.CitaHistorialEstado
{
    public class UpdateCitaHistorialEstadoUseCase
    {
        private readonly IGenericRepository<CitaHistorialEstadoEntity> _repo;

        public UpdateCitaHistorialEstadoUseCase(IGenericRepository<CitaHistorialEstadoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, CreateCitaHistorialEstadoDto dto)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            entity.Update(
                dto.CitaId,
                dto.EstadoAnteriorId,
                dto.EstadoNuevoId,
                dto.UsuarioId,
                dto.Observacion);
            await _repo.UpdateAsync(entity);
        }
    }
}
