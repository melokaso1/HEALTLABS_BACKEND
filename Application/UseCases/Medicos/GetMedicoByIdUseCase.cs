using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Medicos
{
    public class GetMedicoByIdUseCase
    {
        private readonly IGenericRepository<MedicoEntity> _repo;

        public GetMedicoByIdUseCase(IGenericRepository<MedicoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<MedicoEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id);

            return entity;
        }
    }
}