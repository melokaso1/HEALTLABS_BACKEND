using Application.DTOs.DetalleCita;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.DetalleCita;

public sealed class UpdateDetalleCitaUseCase
{
    private readonly IGenericRepository<DetalleCitaEntity> _detalles;
    private readonly IGenericRepository<CitaEntity> _citas;

    public UpdateDetalleCitaUseCase(
        IGenericRepository<DetalleCitaEntity> detalles,
        IGenericRepository<CitaEntity> citas)
    {
        _detalles = detalles;
        _citas = citas;
    }

    public async Task ExecuteAsync(Guid id, UpdateDetalleCitaDto dto)
    {
        var detalle = await _detalles.GetEntityByIdAsync(id)
            ?? throw new KeyNotFoundException("El detalle de cita no existe.");

        var cita = await _citas.GetEntityByIdAsync(dto.CitaId)
            ?? throw new InvalidOperationException("La cita no existe.");

        if (cita.MedicoId != dto.MedicoId)
            throw new InvalidOperationException("El detalle debe corresponder al médico de la cita.");

        detalle.Update(dto.CitaId, dto.MedicoId, dto.NotaAtencion, dto.ResumenConsulta);
        await _detalles.UpdateAsync(detalle);
    }
}
