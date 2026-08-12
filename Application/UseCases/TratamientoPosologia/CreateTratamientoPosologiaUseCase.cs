using Application.DTOs.TratamientoPosologia;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TratamientoPosologia
{
    public class CreateTratamientoPosologiaUseCase
    {
        private readonly IGenericRepository<TratamientoPosologiaEntity> _repo;

        public CreateTratamientoPosologiaUseCase(IGenericRepository<TratamientoPosologiaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<TratamientoPosologiaEntity> ExecuteAsync(CreateTratamientoPosologiaDto dto)
        {
            var entity = new TratamientoPosologiaEntity(
                dto.TratamientoId,
                dto.Dosis,
                dto.Frecuencia,
                dto.DuracionDias,
                dto.Indicaciones);
            return await _repo.AddAsync(entity);
        }
    }
}
