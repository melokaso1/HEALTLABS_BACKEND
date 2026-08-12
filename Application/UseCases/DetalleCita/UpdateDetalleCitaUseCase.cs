using Application.DTOs.DetalleCita;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.DetalleCita;

public sealed class UpdateDetalleCitaUseCase
{
    private readonly IGenericRepository<DetalleCitaEntity> _repo;

    public UpdateDetalleCitaUseCase(IGenericRepository<DetalleCitaEntity> repo) => _repo = repo;

    public async Task ExecuteAsync(Guid id, UpdateDetalleCitaDto dto)
    {
        var entity = await _repo.GetEntityByIdAsync(id)
            ?? throw new KeyNotFoundException("El detalle de cita no existe.");

        entity.Update(dto.NotaAtencion, dto.ResumenConsulta);
        await _repo.UpdateAsync(entity);
    }
}
