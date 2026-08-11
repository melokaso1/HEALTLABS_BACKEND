using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Especialidad
{
    public class GetAllEspecialidadUseCase
    {
        private readonly IGenericRepository<EspecialidadEntity> _repo;

        public GetAllEspecialidadUseCase(IGenericRepository<EspecialidadEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<EspecialidadEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}