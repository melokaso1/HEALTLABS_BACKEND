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
            var empleado = await _repo.GetEntityByIdAsync(id);

            if (empleado == null)
            {
                throw new ArgumentException("No Existe");
            }

            empleado.PersonaId = dto.PersonaId;
            empleado.CargoId = dto.CargoId;
            empleado.FechaIngreso = dto.FechaIngreso;
            empleado.FechaRetiro = dto.FechaRetiro;
            empleado.Activo = dto.Activo;

            await _repo.UpdateAsync(empleado);

        }
    }
}