using Application.DTOs.AtencionTratamiento;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.AtencionTratamiento
{
    public class CreateAtencionTratamientoUseCase
    {
        private readonly IGenericRepository<AtencionTratamientoEntity> _repo;

        public CreateAtencionTratamientoUseCase(IGenericRepository<AtencionTratamientoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<AtencionTratamientoEntity> ExecuteAsync(CreateAtencionTratamientoDto dto)
        {
            var entity = new AtencionTratamientoEntity(
                dto.DetalleCitaId,
                dto.TratamientoId,
                dto.Dosis,
                dto.Frecuencia,
                dto.DuracionDias,
                dto.Indicaciones);

            return await _repo.AddAsync(entity);
        }
    }
}
