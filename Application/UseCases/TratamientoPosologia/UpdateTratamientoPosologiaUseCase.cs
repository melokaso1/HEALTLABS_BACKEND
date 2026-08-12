using Application.DTOs.TratamientoPosologia;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TratamientoPosologia
{
    public class UpdateTratamientoPosologiaUseCase
    {
        private readonly IGenericRepository<TratamientoPosologiaEntity> _repo;

        public UpdateTratamientoPosologiaUseCase(IGenericRepository<TratamientoPosologiaEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateTratamientoPosologiaDto dto)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            entity.Update(
                dto.TratamientoId,
                dto.Dosis,
                dto.Frecuencia,
                dto.DuracionDias,
                dto.Indicaciones);
            await _repo.UpdateAsync(entity);
        }
    }
}
