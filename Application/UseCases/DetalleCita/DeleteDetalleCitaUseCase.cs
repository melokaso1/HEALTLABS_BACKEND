using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.DetalleCita;

public sealed class DeleteDetalleCitaUseCase
{
    private readonly IGenericRepository<DetalleCitaEntity> _repo;

    public DeleteDetalleCitaUseCase(IGenericRepository<DetalleCitaEntity> repo) => _repo = repo;

    public async Task ExecuteAsync(Guid id)
    {
        if (await _repo.GetEntityByIdAsync(id) is null)
            throw new KeyNotFoundException("El detalle de cita no existe.");

        await _repo.DeleteAsync(id);
    }
}
