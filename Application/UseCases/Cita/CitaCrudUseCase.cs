using Application.DTOs.Cita;
using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Cita;

public sealed class CitaCrudUseCase : EntityCrudUseCase<CitaEntity>
{
    private readonly IGenericRepository<CitaEntity> _citas;
    private readonly IGenericRepository<PacienteEntity> _pacientes;
    private readonly IGenericRepository<MedicoEntity> _medicos;
    private readonly IGenericRepository<EstadoCitaEntity> _estados;
    private readonly IGenericRepository<TipoCitaEntity> _tipos;
    private readonly IGenericRepository<UsuarioEntity> _usuarios;
    private readonly IGenericRepository<HorarioEntity> _horarios;
    private readonly IGenericRepository<CitaHistorialEstadoEntity> _historial;

    public CitaCrudUseCase(
        IGenericRepository<CitaEntity> citas,
        IGenericRepository<PacienteEntity> pacientes,
        IGenericRepository<MedicoEntity> medicos,
        IGenericRepository<EstadoCitaEntity> estados,
        IGenericRepository<TipoCitaEntity> tipos,
        IGenericRepository<UsuarioEntity> usuarios,
        IGenericRepository<HorarioEntity> horarios,
        IGenericRepository<CitaHistorialEstadoEntity> historial)
        : base(citas)
    {
        _citas = citas;
        _pacientes = pacientes;
        _medicos = medicos;
        _estados = estados;
        _tipos = tipos;
        _usuarios = usuarios;
        _horarios = horarios;
        _historial = historial;
    }

    public async Task<CitaEntity> CreateAsync(CreateCitaDto dto)
    {
        await ValidateReferencesAsync(dto.PacienteId, dto.MedicoId, dto.EstadoCitaId, dto.TipoCitaId, dto.UsuarioCreacionId);
        await EnsureWithinHorarioAsync(dto.MedicoId, dto.Fecha, dto.HoraInicio, dto.HoraFin);
        await EnsureNoOverlapAsync(dto.MedicoId, dto.Fecha, dto.HoraInicio, dto.HoraFin, excludeCitaId: null);

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

    public async Task UpdateAsync(Guid id, UpdateCitaDto dto)
    {
        var cita = await _citas.GetEntityByIdAsync(id)
            ?? throw new KeyNotFoundException("La cita no existe.");

        await ValidateReferencesAsync(dto.PacienteId, dto.MedicoId, dto.EstadoCitaId, dto.TipoCitaId, cita.UsuarioCreacionId);
        await EnsureWithinHorarioAsync(dto.MedicoId, dto.Fecha, dto.HoraInicio, dto.HoraFin);
        await EnsureNoOverlapAsync(dto.MedicoId, dto.Fecha, dto.HoraInicio, dto.HoraFin, excludeCitaId: id);

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

    public async Task CancelAsync(Guid id, CancelCitaDto dto)
    {
        var cita = await _citas.GetEntityByIdAsync(id)
            ?? throw new KeyNotFoundException("La cita no existe.");

        var cancelada = await GetEstadoByCodigoAsync("CANCELADA");
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

    public async Task ReprogramarAsync(Guid id, ReprogramarCitaDto dto)
    {
        var cita = await _citas.GetEntityByIdAsync(id)
            ?? throw new KeyNotFoundException("La cita no existe.");

        var cancelada = await GetEstadoByCodigoAsync("CANCELADA");
        if (cita.EstadoCitaId == cancelada.Id)
            throw new InvalidOperationException("No se puede reprogramar una cita cancelada.");

        await EnsureWithinHorarioAsync(cita.MedicoId, dto.Fecha, dto.HoraInicio, dto.HoraFin);
        await EnsureNoOverlapAsync(cita.MedicoId, dto.Fecha, dto.HoraInicio, dto.HoraFin, excludeCitaId: id);

        var estadoAnterior = cita.EstadoCitaId;
        var agendada = await GetEstadoByCodigoAsync("AGENDADA");

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

    public new async Task DeleteAsync(Guid id)
    {
        await CancelAsync(id, new CancelCitaDto
        {
            MotivoCancelacion = "Cancelada por usuario",
            UsuarioCancelacionId = null
        });
    }

    private async Task ValidateReferencesAsync(
        Guid pacienteId,
        Guid medicoId,
        Guid estadoId,
        Guid tipoId,
        Guid usuarioId)
    {
        if (!await _pacientes.AnyAsync(p => p.Id == pacienteId && p.Activo))
            throw new InvalidOperationException("El paciente no existe o se encuentra inactivo.");

        if (!await _medicos.AnyAsync(m => m.Id == medicoId && m.Activo))
            throw new InvalidOperationException("El médico no existe o se encuentra inactivo.");

        if (!await _estados.AnyAsync(e => e.Id == estadoId))
            throw new InvalidOperationException("El estado de cita especificado no existe.");

        if (!await _tipos.AnyAsync(t => t.Id == tipoId && t.Activo))
            throw new InvalidOperationException("El tipo de cita especificado no existe o está inactivo.");

        if (!await _usuarios.AnyAsync(u => u.Id == usuarioId))
            throw new InvalidOperationException("El usuario creador especificado no existe.");
    }

    private async Task EnsureNoOverlapAsync(
        Guid medicoId,
        DateOnly fecha,
        TimeOnly horaInicio,
        TimeOnly horaFin,
        Guid? excludeCitaId)
    {
        if (horaFin <= horaInicio)
            throw new InvalidOperationException("La hora de fin debe ser posterior a la hora de inicio.");

        var cancelada = await _estados.FirstOrDefaultAsync(e => e.Codigo == "CANCELADA");
        var canceladaId = cancelada?.Id;

        var overlap = await _citas.AnyAsync(c =>
            c.MedicoId == medicoId &&
            c.Fecha == fecha &&
            (!excludeCitaId.HasValue || c.Id != excludeCitaId.Value) &&
            (canceladaId == null || c.EstadoCitaId != canceladaId) &&
            c.HoraInicio < horaFin &&
            c.HoraFin > horaInicio);

        if (overlap)
            throw new InvalidOperationException("El médico ya tiene una cita agendada en ese rango horario.");
    }

    private async Task EnsureWithinHorarioAsync(
        Guid medicoId,
        DateOnly fecha,
        TimeOnly horaInicio,
        TimeOnly horaFin)
    {
        var horario = await _horarios.FirstOrDefaultAsync(h => h.MedicoId == medicoId && h.Fecha == fecha);
        if (horario is null)
            throw new InvalidOperationException("El médico no tiene horario configurado para la fecha indicada.");

        if (horaInicio < horario.HoraEntrada || horaFin > horario.HoraSalida)
            throw new InvalidOperationException("La cita está fuera del horario laboral del médico.");

        var overlapsLunch = horaInicio < horario.RetornoActividades && horaFin > horario.SalidaAlmuerzo;
        if (overlapsLunch)
            throw new InvalidOperationException("La cita no puede cruzar el horario de almuerzo.");
    }

    private async Task<EstadoCitaEntity> GetEstadoByCodigoAsync(string codigo)
    {
        return await _estados.FirstOrDefaultAsync(e => e.Codigo == codigo)
            ?? throw new InvalidOperationException($"No existe el estado de cita '{codigo}'.");
    }
}
