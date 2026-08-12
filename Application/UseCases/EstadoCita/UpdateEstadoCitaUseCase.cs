using Application.DTOs.EstadoCita;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.EstadoCita
{
    public class UpdateEstadoCitaUseCase
    {
        private readonly IGenericRepository<EstadoCitaEntity> _repo;

        public UpdateEstadoCitaUseCase(IGenericRepository<EstadoCitaEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateEstadoCitaDto dto)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            entity.Update(dto.Descripcion);
            await _repo.UpdateAsync(entity);
        }
    }
}
