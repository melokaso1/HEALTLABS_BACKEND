using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Citas
{
    public class DeleteCitaUseCase
    {
        private readonly IGenericRepository<CitaEntity> _repo;
        public DeleteCitaUseCase(IGenericRepository<CitaEntity> repo)
        {
            _repo = repo;
        }
        public async Task ExecuteAsync(Guid id)
        { 

           await _repo.DeleteAsync(id);
        }
    }
}
