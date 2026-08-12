using Application.DTOs.Cita;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Citas;

public sealed class MarcarNoAsistioCitaUseCase
{
    private readonly IGenericRepository<CitaEntity> _citas;
    private readonly IGenericRepository<CitaHistorialEstadoEntity> _historial;
    private readonly CitaSchedulingRules _rules;

    public MarcarNoAsistioCitaUseCase(
        IGenericRepository<CitaEntity> citas,
        IGenericRepository<CitaHistorialEstadoEntity> historial,
        CitaSchedulingRules rules)
    {
        _citas = citas;
        _historial = historial;
        _rules = rules;
    }

    public async Task ExecuteAsync(Guid id, MarcarNoAsistioCitaDto dto)
    {
        var cita = await _citas.GetEntityByIdAsync(id)
            ?? throw new KeyNotFoundException("La cita no existe.");

        var noAsistio = await _rules.GetEstadoByCodigoAsync("NO_ASISTIO");
        var cancelada = await _rules.GetEstadoByCodigoAsync("CANCELADA");
        var atendida = await _rules.GetEstadoByCodigoAsync("ATENDIDA");

        if (cita.EstadoCitaId == noAsistio.Id)
            throw new InvalidOperationException("La cita ya está marcada como no asistió.");

        if (cita.EstadoCitaId == cancelada.Id)
            throw new InvalidOperationException("No se puede marcar no asistió una cita cancelada.");

        if (cita.EstadoCitaId == atendida.Id)
            throw new InvalidOperationException("No se puede marcar no asistió una cita atendida.");

        var estadoAnterior = cita.EstadoCitaId;
        cita.MarcarNoAsistio(noAsistio.Id);

        await _citas.UpdateAsync(cita);
        await _historial.AddAsync(new CitaHistorialEstadoEntity(
            cita.Id,
            estadoAnterior,
            noAsistio.Id,
            dto.UsuarioId,
            dto.Observacion ?? "Paciente no asistió"));
    }
}
