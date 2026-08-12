using Application.DTOs.Reportes;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Reportes;

public sealed class GetCitasPorRangoUseCase
{
    private readonly IGenericRepository<CitaEntity> _citas;
    private readonly IGenericRepository<EstadoCitaEntity> _estados;

    public GetCitasPorRangoUseCase(
        IGenericRepository<CitaEntity> citas,
        IGenericRepository<EstadoCitaEntity> estados)
    {
        _citas = citas;
        _estados = estados;
    }

    public async Task<IEnumerable<CitaReporteDto>> ExecuteAsync(DateOnly desde, DateOnly hasta, Guid? medicoId = null)
    {
        var citas = await _citas.FindAsync(c =>
            c.Fecha >= desde &&
            c.Fecha <= hasta &&
            (!medicoId.HasValue || c.MedicoId == medicoId.Value));

        var estados = (await _estados.GetAllEntitiesAsync()).ToDictionary(e => e.Id, e => e.Codigo);

        return citas
            .OrderBy(c => c.Fecha)
            .ThenBy(c => c.HoraInicio)
            .Select(c => new CitaReporteDto
            {
                CitaId = c.Id,
                Fecha = c.Fecha,
                HoraInicio = c.HoraInicio,
                HoraFin = c.HoraFin,
                MedicoId = c.MedicoId,
                PacienteId = c.PacienteId,
                EstadoCitaId = c.EstadoCitaId,
                EstadoCodigo = estados.GetValueOrDefault(c.EstadoCitaId),
                MotivoConsulta = c.MotivoConsulta
            });
    }
}
