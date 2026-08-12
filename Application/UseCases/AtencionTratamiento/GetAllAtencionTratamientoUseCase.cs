using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.AtencionTratamiento
{
    public class GetAllAtencionTratamientoUseCase
    {
        private readonly IGenericRepository<AtencionTratamientoEntity> _repo;

        public GetAllAtencionTratamientoUseCase(IGenericRepository<AtencionTratamientoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<AtencionTratamientoEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}
