using Domain.Entities;
using Domain.Interfaces;


namespace Application.UseCases.Usuarios
{
    public class GetAllUsuarioUseCase
    {
        private readonly IGenericRepository<UsuarioEntity> _repo;

        public GetAllUsuarioUseCase(IGenericRepository<UsuarioEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<UsuarioEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}

