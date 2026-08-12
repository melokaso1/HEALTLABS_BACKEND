using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.RolPermiso
{
    public class GetRolPermisoByIdUseCase
    {
        private readonly IGenericRepository<RolPermisoEntity> _repo;

        public GetRolPermisoByIdUseCase(IGenericRepository<RolPermisoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<RolPermisoEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El permiso del rol no existe.");
            return entity;
        }
    }
}
