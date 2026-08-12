using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Citas;

public sealed class GetCitaByIdUseCase
{
    private readonly IGenericRepository<CitaEntity> _repo;

    public GetCitaByIdUseCase(IGenericRepository<CitaEntity> repo) => _repo = repo;

    public Task<CitaEntity?> ExecuteAsync(Guid id) => _repo.GetEntityByIdAsync(id);
}
