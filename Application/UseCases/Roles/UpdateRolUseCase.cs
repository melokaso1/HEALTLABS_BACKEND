using Application.DTOs.Rol;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Roles
{
    public class UpdateRolUseCase
    {
        private readonly IGenericRepository<RolEntity> _repo;

        public UpdateRolUseCase(IGenericRepository<RolEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateRolDto dto)
        {
            var rol = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            rol.Update(dto.NombreRol, dto.Descripcion, dto.Activo);

            await _repo.UpdateAsync(rol);
        }
    }
}
