using Application.DTOs.Reportes;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Reportes;

public sealed class GetConteoCitasPorMedicoUseCase
{
    private readonly IGenericRepository<CitaEntity> _citas;

    public GetConteoCitasPorMedicoUseCase(IGenericRepository<CitaEntity> citas)
    {
        _citas = citas;
    }

    public async Task<IEnumerable<ConteoPorMedicoDto>> ExecuteAsync(DateOnly? desde = null, DateOnly? hasta = null)
    {
        var citas = await _citas.FindAsync(c =>
            (!desde.HasValue || c.Fecha >= desde.Value) &&
            (!hasta.HasValue || c.Fecha <= hasta.Value));

        return citas
            .GroupBy(c => c.MedicoId)
            .Select(g => new ConteoPorMedicoDto
            {
                MedicoId = g.Key,
                Total = g.Count()
            })
            .OrderByDescending(x => x.Total);
    }
}
