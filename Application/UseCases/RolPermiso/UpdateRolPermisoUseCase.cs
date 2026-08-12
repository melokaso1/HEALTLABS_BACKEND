using Application.DTOs.RolPermiso;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.RolPermiso
{
    public class UpdateRolPermisoUseCase
    {
        private readonly IGenericRepository<RolPermisoEntity> _repo;

        public UpdateRolPermisoUseCase(IGenericRepository<RolPermisoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, CreateRolPermisoDto dto)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            entity.Update(dto.RolId, dto.PermisoId);
            await _repo.UpdateAsync(entity);
        }
    }
}
