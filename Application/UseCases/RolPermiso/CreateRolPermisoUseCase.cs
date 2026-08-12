using Application.DTOs.RolPermiso;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.RolPermiso
{
    public class CreateRolPermisoUseCase
    {
        private readonly IGenericRepository<RolPermisoEntity> _repo;

        public CreateRolPermisoUseCase(IGenericRepository<RolPermisoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<RolPermisoEntity> ExecuteAsync(CreateRolPermisoDto dto)
        {
            var entity = new RolPermisoEntity(dto.RolId, dto.PermisoId);
            return await _repo.AddAsync(entity);
        }
    }
}
