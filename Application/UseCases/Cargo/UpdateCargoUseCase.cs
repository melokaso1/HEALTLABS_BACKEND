using Application.DTOs.Cargo;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Cargo
{
    public class UpdateCargoUseCase
    {
        private readonly IGenericRepository<CargoEntity> _repo;

        public UpdateCargoUseCase(IGenericRepository<CargoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateCargoDto dto)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            entity.Update(dto.Nombre, dto.Descripcion, dto.NivelJerarquico);
            await _repo.UpdateAsync(entity);
        }
    }
}
