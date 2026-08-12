using Application.DTOs.Reportes;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Reportes;

public sealed class GetConteoCitasPorEstadoUseCase
{
    private readonly IGenericRepository<CitaEntity> _citas;
    private readonly IGenericRepository<EstadoCitaEntity> _estados;

    public GetConteoCitasPorEstadoUseCase(
        IGenericRepository<CitaEntity> citas,
        IGenericRepository<EstadoCitaEntity> estados)
    {
        _citas = citas;
        _estados = estados;
    }

    public async Task<IEnumerable<ConteoPorEstadoDto>> ExecuteAsync(DateOnly? desde = null, DateOnly? hasta = null)
    {
        var citas = await _citas.FindAsync(c =>
            (!desde.HasValue || c.Fecha >= desde.Value) &&
            (!hasta.HasValue || c.Fecha <= hasta.Value));

        var estados = (await _estados.GetAllEntitiesAsync()).ToDictionary(e => e.Id, e => e.Codigo);

        return citas
            .GroupBy(c => c.EstadoCitaId)
            .Select(g => new ConteoPorEstadoDto
            {
                EstadoCitaId = g.Key,
                EstadoCodigo = estados.GetValueOrDefault(g.Key),
                Total = g.Count()
            })
            .OrderByDescending(x => x.Total);
    }
}
