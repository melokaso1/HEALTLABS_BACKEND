using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.AtencionTratamiento
{
    public class GetAtencionTratamientoByIdUseCase
    {
        private readonly IGenericRepository<AtencionTratamientoEntity> _repo;

        public GetAtencionTratamientoByIdUseCase(IGenericRepository<AtencionTratamientoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<AtencionTratamientoEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("La atención de tratamiento no existe.");
            return entity;
        }
    }
}