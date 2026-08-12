using Application.DTOs.Cita;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Citas;

public sealed class CreateCitaUseCase
{
    private readonly IGenericRepository<CitaEntity> _citas;
    private readonly IGenericRepository<CitaHistorialEstadoEntity> _historial;
    private readonly CitaSchedulingRules _rules;

    public CreateCitaUseCase(
        IGenericRepository<CitaEntity> citas,
        IGenericRepository<CitaHistorialEstadoEntity> historial,
        CitaSchedulingRules rules)
    {
        _citas = citas;
        _historial = historial;
        _rules = rules;
    }

    public async Task<CitaEntity> ExecuteAsync(CreateCitaDto dto)
    {
        await _rules.ValidateReferencesAsync(dto.PacienteId, dto.MedicoId, dto.EstadoCitaId, dto.TipoCitaId, dto.UsuarioCreacionId);
        await _rules.EnsureWithinHorarioAsync(dto.MedicoId, dto.HoraInicio, dto.HoraFin);
        await _rules.EnsureNoOverlapAsync(dto.MedicoId, dto.Fecha, dto.HoraInicio, dto.HoraFin, excludeCitaId: null);

        var cita = new CitaEntity(
            dto.PacienteId,
            dto.MedicoId,
            dto.EstadoCitaId,
            dto.TipoCitaId,
            dto.Fecha,
            dto.HoraInicio,
            dto.HoraFin,
            dto.MotivoConsulta,
            dto.Observaciones,
            dto.UsuarioCreacionId);

        var created = await _citas.AddAsync(cita);

        await _historial.AddAsync(new CitaHistorialEstadoEntity(
            created.Id,
            created.EstadoCitaId,
            created.EstadoCitaId,
            dto.UsuarioCreacionId,
            "Cita creada"));

        return created;
    }
}
