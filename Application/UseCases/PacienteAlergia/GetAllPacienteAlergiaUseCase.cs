using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PacienteAlergia
{
    public class GetAllPacienteAlergiaUseCase
    {
        private readonly IGenericRepository<PacienteAlergiaEntity> _repo;

        public GetAllPacienteAlergiaUseCase(IGenericRepository<PacienteAlergiaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PacienteAlergiaEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}