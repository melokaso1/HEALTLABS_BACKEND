using Application.DTOs.TipoCita;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TipoCita
{
    public class UpdateTipoCitaUseCase
    {
        private readonly IGenericRepository<TipoCitaEntity> _repo;

        public UpdateTipoCitaUseCase(IGenericRepository<TipoCitaEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateTipoCitaDto dto)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            entity.Update(dto.Codigo, dto.Nombre, dto.DuracionMinutos, dto.Activo);
            await _repo.UpdateAsync(entity);
        }
    }
}
