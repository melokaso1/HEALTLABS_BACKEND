using Application.DTOs.Cargo;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Cargo
{
    public class CreateCargoUseCase
    {
        private readonly IGenericRepository<CargoEntity> _repo;

        public CreateCargoUseCase(IGenericRepository<CargoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<CargoEntity> ExecuteAsync(CreateCargoDto dto)
        {
            var entity = new CargoEntity(dto.Codigo, dto.Nombre, dto.Descripcion, dto.NivelJerarquico);
            return await _repo.AddAsync(entity);
        }
    }
}
