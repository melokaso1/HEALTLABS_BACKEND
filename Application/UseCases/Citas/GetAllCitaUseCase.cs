using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Citas;

public sealed class GetAllCitaUseCase
{
    private readonly IGenericRepository<CitaEntity> _repo;

    public GetAllCitaUseCase(IGenericRepository<CitaEntity> repo) => _repo = repo;

    public Task<IEnumerable<CitaEntity>> ExecuteAsync(
        Guid? medicoId = null,
        DateOnly? desde = null,
        DateOnly? hasta = null)
    {
        if (desde.HasValue && hasta.HasValue && desde > hasta)
            throw new ArgumentException("La fecha 'desde' no puede ser posterior a 'hasta'.");

        if (medicoId.HasValue || desde.HasValue || hasta.HasValue)
        {
            return _repo.FindAsync(c =>
                (!medicoId.HasValue || c.MedicoId == medicoId.Value) &&
                (!desde.HasValue || c.Fecha >= desde.Value) &&
                (!hasta.HasValue || c.Fecha <= hasta.Value));
        }

        return _repo.GetAllEntitiesAsync();
    }
}
