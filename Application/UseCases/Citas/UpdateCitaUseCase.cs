using Application.DTOs.Cita;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Citas;

public sealed class UpdateCitaUseCase
{
    private readonly IGenericRepository<CitaEntity> _citas;
    private readonly IGenericRepository<CitaHistorialEstadoEntity> _historial;
    private readonly CitaSchedulingRules _rules;

    public UpdateCitaUseCase(
        IGenericRepository<CitaEntity> citas,
        IGenericRepository<CitaHistorialEstadoEntity> historial,
        CitaSchedulingRules rules)
    {
        _citas = citas;
        _historial = historial;
        _rules = rules;
    }

    public async Task ExecuteAsync(Guid id, UpdateCitaDto dto)
    {
        var cita = await _citas.GetEntityByIdAsync(id)
            ?? throw new KeyNotFoundException("La cita no existe.");

        await _rules.ValidateReferencesAsync(dto.PacienteId, dto.MedicoId, dto.EstadoCitaId, dto.TipoCitaId, cita.UsuarioCreacionId);
        await _rules.EnsureWithinHorarioAsync(dto.MedicoId, dto.HoraInicio, dto.HoraFin);
        await _rules.EnsureNoOverlapAsync(dto.MedicoId, dto.Fecha, dto.HoraInicio, dto.HoraFin, excludeCitaId: id);

        var estadoAnterior = cita.EstadoCitaId;

        cita.Update(
            dto.PacienteId,
            dto.MedicoId,
            dto.EstadoCitaId,
            dto.TipoCitaId,
            dto.Fecha,
            dto.HoraInicio,
            dto.HoraFin,
            dto.MotivoConsulta,
            dto.Observaciones,
            cita.UsuarioCreacionId,
            dto.MotivoCancelacion ?? cita.MotivoCancelacion,
            dto.UsuarioCancelacionId ?? cita.UsuarioCancelacionId,
            dto.FechaCancelacion ?? cita.FechaCancelacion);

        await _citas.UpdateAsync(cita);

        if (estadoAnterior != dto.EstadoCitaId)
        {
            await _historial.AddAsync(new CitaHistorialEstadoEntity(
                cita.Id,
                estadoAnterior,
                dto.EstadoCitaId,
                dto.UsuarioCancelacionId ?? cita.UsuarioCreacionId,
                "Cambio de estado"));
        }
    }
}
