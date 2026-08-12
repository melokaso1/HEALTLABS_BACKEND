using Application.DTOs.DetalleDiagnostico;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.DetalleDiagnostico
{
    public class UpdateDetalleDiagnosticoUseCase
    {
        private readonly IGenericRepository<DetalleDiagnosticoEntity> _repo;

        public UpdateDetalleDiagnosticoUseCase(IGenericRepository<DetalleDiagnosticoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateDetalleDiagnosticoDto dto)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            entity.Update(dto.DetalleCitaId, dto.DiagnosticoId, dto.Principal);

            await _repo.UpdateAsync(entity);
        }
    }
}
