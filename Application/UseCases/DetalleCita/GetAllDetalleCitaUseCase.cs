using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.DetalleCita;

public sealed class GetAllDetalleCitaUseCase
{
    private readonly IGenericRepository<DetalleCitaEntity> _repo;

    public GetAllDetalleCitaUseCase(IGenericRepository<DetalleCitaEntity> repo) => _repo = repo;

    public Task<IEnumerable<DetalleCitaEntity>> ExecuteAsync() => _repo.GetAllEntitiesAsync();
}
