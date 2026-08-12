using Application.DTOs.Permiso;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Permiso
{
    public class CreatePermisoUseCase
    {
        private readonly IGenericRepository<PermisoEntity> _repo;

        public CreatePermisoUseCase(IGenericRepository<PermisoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<PermisoEntity> ExecuteAsync(CreatePermisoDto dto)
        {
            var entity = new PermisoEntity(dto.Codigo, dto.Modulo, dto.Descripcion);
            return await _repo.AddAsync(entity);
        }
    }
}
