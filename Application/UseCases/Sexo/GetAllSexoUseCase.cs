using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Sexo
{
    public class GetAllSexoUseCase
    {
        private readonly IGenericRepository<SexoEntity> _repo;

        public GetAllSexoUseCase(IGenericRepository<SexoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<SexoEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}
