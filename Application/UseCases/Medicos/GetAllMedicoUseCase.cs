using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Medicos
{
    public class GetAllMedicoUseCase
    {
        private readonly IGenericRepository<MedicoEntity> _repo;

        public GetAllMedicoUseCase(IGenericRepository<MedicoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<MedicoEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}