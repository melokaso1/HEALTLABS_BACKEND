using Application.DTOs.Rol;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Roles
{
    public class CreateRolUseCase
    {
        private readonly IGenericRepository<RolEntity> _repo;

        public CreateRolUseCase(IGenericRepository<RolEntity> repo)
        {
            _repo = repo;
        }

        public async Task<RolEntity> ExecuteAsync(CreateRolDto dto)
        {
            var rol = new RolEntity(
                dto.NombreRol,
                dto.Descripcion,
                dto.Activo);

            return await _repo.AddAsync(rol);
        }
    }
}
