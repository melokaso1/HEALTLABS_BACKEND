using Application.DTOs.Cita;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Citas;

public sealed class CancelCitaUseCase
{
    private readonly IGenericRepository<CitaEntity> _citas;
    private readonly IGenericRepository<CitaHistorialEstadoEntity> _historial;
    private readonly CitaSchedulingRules _rules;

    public CancelCitaUseCase(
        IGenericRepository<CitaEntity> citas,
        IGenericRepository<CitaHistorialEstadoEntity> historial,
        CitaSchedulingRules rules)
    {
        _citas = citas;
        _historial = historial;
        _rules = rules;
    }

    public async Task ExecuteAsync(Guid id, CancelCitaDto dto)
    {
        var cita = await _citas.GetEntityByIdAsync(id)
            ?? throw new KeyNotFoundException("La cita no existe.");

        var cancelada = await _rules.GetEstadoByCodigoAsync("CANCELADA");
        if (cita.EstadoCitaId == cancelada.Id)
            throw new InvalidOperationException("La cita ya está cancelada.");

        var estadoAnterior = cita.EstadoCitaId;
        cita.Update(
            cita.PacienteId,
            cita.MedicoId,
            cancelada.Id,
            cita.TipoCitaId,
            cita.Fecha,
            cita.HoraInicio,
            cita.HoraFin,
            cita.MotivoConsulta,
            cita.Observaciones,
            cita.UsuarioCreacionId,
            dto.MotivoCancelacion,
            dto.UsuarioCancelacionId,
            DateTime.UtcNow);

        await _citas.UpdateAsync(cita);
        await _historial.AddAsync(new CitaHistorialEstadoEntity(
            cita.Id,
            estadoAnterior,
            cancelada.Id,
            dto.UsuarioCancelacionId,
            dto.MotivoCancelacion));
    }
}
