using Application.DTOs.Permiso;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Permiso
{
    public class UpdatePermisoUseCase
    {
        private readonly IGenericRepository<PermisoEntity> _repo;

        public UpdatePermisoUseCase(IGenericRepository<PermisoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdatePermisoDto dto)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            entity.Update(dto.Codigo, dto.Modulo, dto.Descripcion);
            await _repo.UpdateAsync(entity);
        }
    }
}
