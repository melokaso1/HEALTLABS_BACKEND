using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Empleados
{
    public class GetEmpleadoByIdUseCase
    {
        private readonly IGenericRepository<EmpleadoEntity> _repo;

        public GetEmpleadoByIdUseCase(IGenericRepository<EmpleadoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<EmpleadoEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El empleado no existe.");
            return entity;
        }
    }
}