using Application.DTOs.Tratamiento;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Tratamiento
{
    public class UpdateTratamientoUseCase
    {
        private readonly IGenericRepository<TratamientoEntity> _repo;

        public UpdateTratamientoUseCase(IGenericRepository<TratamientoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateTratamientoDto dto)
        {
            var tratamiento = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            tratamiento.Update(dto.Nombre, dto.Descripcion, dto.Activo);
            await _repo.UpdateAsync(tratamiento);
        }
    }
}
