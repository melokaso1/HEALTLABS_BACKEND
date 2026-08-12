using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Horarios
{
    public class GetHorariosByMedicoIdUseCase
    {
        private readonly IGenericRepository<HorarioEntity> _repo;

        public GetHorariosByMedicoIdUseCase(IGenericRepository<HorarioEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<HorarioEntity>> ExecuteAsync(Guid medicoId)
        {
            return await _repo.FindAsync(h => h.MedicoId == medicoId);
        }
    }
}
