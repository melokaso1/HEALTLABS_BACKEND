using Application.DTOs.Empleado;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Empleados
{
    public class UpdateEmpleadoUseCase
    {
        private readonly IGenericRepository<EmpleadoEntity> _repo;

        public UpdateEmpleadoUseCase(IGenericRepository<EmpleadoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateEmpleadoDto dto)
        {
            var empleado = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            empleado.Update(dto.CargoId, dto.FechaIngreso, dto.FechaRetiro, dto.Activo);
            await _repo.UpdateAsync(empleado);
        }
    }
}
