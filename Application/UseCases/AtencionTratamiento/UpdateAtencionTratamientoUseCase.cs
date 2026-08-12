using Application.DTOs.AtencionTratamiento;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.AtencionTratamiento
{
    public class UpdateAtencionTratamientoUseCase
    {
        private readonly IGenericRepository<AtencionTratamientoEntity> _repo;

        public UpdateAtencionTratamientoUseCase(IGenericRepository<AtencionTratamientoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateAtencionTratamientoDto dto)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            entity.Update(
                dto.DetalleCitaId,
                dto.TratamientoId,
                dto.Dosis,
                dto.Frecuencia,
                dto.DuracionDias,
                dto.Indicaciones);

            await _repo.UpdateAsync(entity);
        }
    }
}
