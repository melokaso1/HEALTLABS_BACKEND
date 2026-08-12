using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Citas;

public sealed class CitaSchedulingRules
{
    private readonly IGenericRepository<CitaEntity> _citas;
    private readonly IGenericRepository<PacienteEntity> _pacientes;
    private readonly IGenericRepository<MedicoEntity> _medicos;
    private readonly IGenericRepository<EstadoCitaEntity> _estados;
    private readonly IGenericRepository<TipoCitaEntity> _tipos;
    private readonly IGenericRepository<UsuarioEntity> _usuarios;
    private readonly IGenericRepository<HorarioEntity> _horarios;

    public CitaSchedulingRules(
        IGenericRepository<CitaEntity> citas,
        IGenericRepository<PacienteEntity> pacientes,
        IGenericRepository<MedicoEntity> medicos,
        IGenericRepository<EstadoCitaEntity> estados,
        IGenericRepository<TipoCitaEntity> tipos,
        IGenericRepository<UsuarioEntity> usuarios,
        IGenericRepository<HorarioEntity> horarios)
    {
        _citas = citas;
        _pacientes = pacientes;
        _medicos = medicos;
        _estados = estados;
        _tipos = tipos;
        _usuarios = usuarios;
        _horarios = horarios;
    }

    public async Task ValidateReferencesAsync(
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

    public async Task EnsureNoOverlapAsync(
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

    public async Task EnsureWithinHorarioAsync(
        Guid medicoId,
        TimeOnly horaInicio,
        TimeOnly horaFin)
    {
        var horario = await _horarios.FirstOrDefaultAsync(h => h.MedicoId == medicoId);
        if (horario is null)
            throw new InvalidOperationException("El médico no tiene jornada configurada.");

        if (horaInicio < horario.HoraEntrada || horaFin > horario.HoraSalida)
            throw new InvalidOperationException("La cita está fuera del horario laboral del médico.");

        var overlapsLunch = horaInicio < horario.RetornoActividades && horaFin > horario.SalidaAlmuerzo;
        if (overlapsLunch)
            throw new InvalidOperationException("La cita no puede cruzar el horario de almuerzo.");
    }

    public async Task<EstadoCitaEntity> GetEstadoByCodigoAsync(string codigo)
    {
        return await _estados.FirstOrDefaultAsync(e => e.Codigo == codigo)
            ?? throw new InvalidOperationException($"No existe el estado de cita '{codigo}'.");
    }
}
