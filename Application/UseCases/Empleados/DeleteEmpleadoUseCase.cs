using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Empleados
{
    public class DeleteEmpleadoUseCase
    {
        private readonly IGenericRepository<EmpleadoEntity> _repo;
        public DeleteEmpleadoUseCase(IGenericRepository<EmpleadoEntity> repo)
        {
            _repo = repo;
        }
        public async Task ExecuteAsync(Guid id)
        {

            await _repo.DeleteAsync(id);
        }
    }
}
