using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.MedicoEspecialidad
{
    public class GetAllMedicoEspecialidadUseCase
    {
        private readonly IGenericRepository<MedicoEspecialidadEntity> _repo;

        public GetAllMedicoEspecialidadUseCase(IGenericRepository<MedicoEspecialidadEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<MedicoEspecialidadEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}
