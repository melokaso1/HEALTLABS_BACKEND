using Application.DTOs.Cita;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Citas;

public sealed class ReprogramarCitaUseCase
{
    private readonly IGenericRepository<CitaEntity> _citas;
    private readonly IGenericRepository<CitaHistorialEstadoEntity> _historial;
    private readonly CitaSchedulingRules _rules;

    public ReprogramarCitaUseCase(
        IGenericRepository<CitaEntity> citas,
        IGenericRepository<CitaHistorialEstadoEntity> historial,
        CitaSchedulingRules rules)
    {
        _citas = citas;
        _historial = historial;
        _rules = rules;
    }

    public async Task ExecuteAsync(Guid id, ReprogramarCitaDto dto)
    {
        var cita = await _citas.GetEntityByIdAsync(id)
            ?? throw new KeyNotFoundException("La cita no existe.");

        var cancelada = await _rules.GetEstadoByCodigoAsync("CANCELADA");
        if (cita.EstadoCitaId == cancelada.Id)
            throw new InvalidOperationException("No se puede reprogramar una cita cancelada.");

        await _rules.EnsureWithinHorarioAsync(cita.MedicoId, dto.HoraInicio, dto.HoraFin);
        await _rules.EnsureNoOverlapAsync(cita.MedicoId, dto.Fecha, dto.HoraInicio, dto.HoraFin, excludeCitaId: id);

        var estadoAnterior = cita.EstadoCitaId;
        var agendada = await _rules.GetEstadoByCodigoAsync("AGENDADA");

        cita.Update(
            cita.PacienteId,
            cita.MedicoId,
            agendada.Id,
            cita.TipoCitaId,
            dto.Fecha,
            dto.HoraInicio,
            dto.HoraFin,
            cita.MotivoConsulta,
            dto.Observaciones ?? cita.Observaciones,
            cita.UsuarioCreacionId,
            null,
            null,
            null);

        await _citas.UpdateAsync(cita);

        if (estadoAnterior != agendada.Id)
        {
            await _historial.AddAsync(new CitaHistorialEstadoEntity(
                cita.Id,
                estadoAnterior,
                agendada.Id,
                dto.UsuarioId,
                "Cita reprogramada"));
        }
        else
        {
            await _historial.AddAsync(new CitaHistorialEstadoEntity(
                cita.Id,
                estadoAnterior,
                cita.EstadoCitaId,
                dto.UsuarioId,
                "Cita reprogramada (mismo estado)"));
        }
    }
}
