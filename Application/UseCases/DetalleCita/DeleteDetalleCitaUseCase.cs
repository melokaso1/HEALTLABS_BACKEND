using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.DetalleCita;

public sealed class DeleteDetalleCitaUseCase
{
    private readonly IGenericRepository<DetalleCitaEntity> _detalles;
    private readonly IGenericRepository<CitaEntity> _citas;
    private readonly IGenericRepository<EstadoCitaEntity> _estados;
    private readonly IGenericRepository<CitaHistorialEstadoEntity> _historial;

    public DeleteDetalleCitaUseCase(
        IGenericRepository<DetalleCitaEntity> detalles,
        IGenericRepository<CitaEntity> citas,
        IGenericRepository<EstadoCitaEntity> estados,
        IGenericRepository<CitaHistorialEstadoEntity> historial)
        => (_detalles, _citas, _estados, _historial) = (detalles, citas, estados, historial);

    public async Task ExecuteAsync(Guid id)
    {
        var detalle = await _detalles.GetEntityByIdAsync(id)
            ?? throw new KeyNotFoundException("El detalle de cita no existe.");
        var cita = await _citas.GetEntityByIdAsync(detalle.CitaId)
            ?? throw new InvalidOperationException("La cita asociada al detalle no existe.");
        var cancelada = await _estados.FirstOrDefaultAsync(e => e.Codigo == "CANCELADA");

        if (cita.EstadoCitaId == cancelada?.Id)
            throw new InvalidOperationException("No se puede eliminar el detalle de una cita cancelada.");

        var atendida = await _estados.FirstOrDefaultAsync(e => e.Codigo == "ATENDIDA");
        if (cita.EstadoCitaId == atendida?.Id)
        {
            var agendada = await _estados.FirstOrDefaultAsync(e => e.Codigo == "AGENDADA")
                ?? throw new InvalidOperationException("No existe el estado AGENDADA.");
            var estadoAnterior = cita.EstadoCitaId;

            cita.CambiarEstado(agendada.Id);
            await _citas.UpdateAsync(cita);
            await _historial.AddAsync(new CitaHistorialEstadoEntity(
                cita.Id,
                estadoAnterior,
                agendada.Id,
                null,
                "Detalle eliminado"));
        }

        await _detalles.DeleteAsync(id);
    }
}
