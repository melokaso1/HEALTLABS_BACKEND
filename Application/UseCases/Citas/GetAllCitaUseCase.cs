using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Citas;

public sealed class GetAllCitaUseCase
{
    private readonly IGenericRepository<CitaEntity> _repo;

    public GetAllCitaUseCase(IGenericRepository<CitaEntity> repo) => _repo = repo;

    public Task<IEnumerable<CitaEntity>> ExecuteAsync(Guid? medicoId = null)
    {
        if (medicoId.HasValue)
            return _repo.FindAsync(c => c.MedicoId == medicoId.Value);

        return _repo.GetAllEntitiesAsync();
    }
}
