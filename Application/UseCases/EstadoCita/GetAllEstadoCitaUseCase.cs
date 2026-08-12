using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.EstadoCita
{
    public class GetAllEstadoCitaUseCase
    {
        private readonly IGenericRepository<EstadoCitaEntity> _repo;

        public GetAllEstadoCitaUseCase(IGenericRepository<EstadoCitaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<EstadoCitaEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}
