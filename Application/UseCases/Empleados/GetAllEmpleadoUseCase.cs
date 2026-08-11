using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Empleados
{
    public class GetAllEmpleadoUseCase
    {
        private readonly IGenericRepository<EmpleadoEntity> _repo;

        public GetAllEmpleadoUseCase(IGenericRepository<EmpleadoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<EmpleadoEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}

