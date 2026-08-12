using Application.DTOs.DetalleCita;
using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.DetalleCita;

public sealed class DetalleCitaCrudUseCase : EntityCrudUseCase<DetalleCitaEntity>
{
    private readonly IGenericRepository<DetalleCitaEntity> _detalles;
    private readonly IGenericRepository<CitaEntity> _citas;
    private readonly IGenericRepository<MedicoEntity> _medicos;
    private readonly IGenericRepository<EstadoCitaEntity> _estados;
    private readonly IGenericRepository<CitaHistorialEstadoEntity> _historial;

    public DetalleCitaCrudUseCase(
        IGenericRepository<DetalleCitaEntity> detalles,
        IGenericRepository<CitaEntity> citas,
        IGenericRepository<MedicoEntity> medicos,
        IGenericRepository<EstadoCitaEntity> estados,
        IGenericRepository<CitaHistorialEstadoEntity> historial)
        : base(detalles)
    {
        _detalles = detalles;
        _citas = citas;
        _medicos = medicos;
        _estados = estados;
        _historial = historial;
    }

    public async Task<DetalleCitaEntity> CreateAsync(CreateDetalleCitaDto dto)
    {
        var cita = await _citas.GetEntityByIdAsync(dto.CitaId)
            ?? throw new InvalidOperationException("La cita no existe.");

        if (!await _medicos.AnyAsync(m => m.Id == dto.MedicoId && m.Activo))
            throw new InvalidOperationException("El médico no existe o está inactivo.");

        if (cita.MedicoId != dto.MedicoId)
            throw new InvalidOperationException("El detalle debe registrarse con el médico asignado a la cita.");

        if (await _detalles.AnyAsync(d => d.CitaId == dto.CitaId))
            throw new InvalidOperationException("La cita ya tiene un detalle de atención.");

        var detalle = new DetalleCitaEntity(dto.CitaId, dto.MedicoId, dto.NotaAtencion, dto.ResumenConsulta);
        var created = await _detalles.AddAsync(detalle);

        var atendida = await _estados.FirstOrDefaultAsync(e => e.Codigo == "ATENDIDA");
        if (atendida is not null && cita.EstadoCitaId != atendida.Id)
        {
            var estadoAnterior = cita.EstadoCitaId;
            cita.Update(
                cita.PacienteId,
                cita.MedicoId,
                atendida.Id,
                cita.TipoCitaId,
                cita.Fecha,
                cita.HoraInicio,
                cita.HoraFin,
                cita.MotivoConsulta,
                cita.Observaciones,
                cita.UsuarioCreacionId,
                cita.MotivoCancelacion,
                cita.UsuarioCancelacionId,
                cita.FechaCancelacion);

            await _citas.UpdateAsync(cita);
            await _historial.AddAsync(new CitaHistorialEstadoEntity(
                cita.Id,
                estadoAnterior,
                atendida.Id,
                null,
                "Atención registrada por profesional"));
        }

        return created;
    }

    public async Task UpdateAsync(Guid id, UpdateDetalleCitaDto dto)
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
