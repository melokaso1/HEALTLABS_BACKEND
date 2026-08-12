using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Citas;

public sealed class GetCitaByIdUseCase
{
    private readonly IGenericRepository<CitaEntity> _repo;

    public GetCitaByIdUseCase(IGenericRepository<CitaEntity> repo)
    {
        _repo = repo;
    }

    public async Task<CitaEntity> ExecuteAsync(Guid id)
    {
        var entity = await _repo.GetEntityByIdAsync(id)
            ?? throw new KeyNotFoundException("La cita no existe.");

        return entity;
    }
}