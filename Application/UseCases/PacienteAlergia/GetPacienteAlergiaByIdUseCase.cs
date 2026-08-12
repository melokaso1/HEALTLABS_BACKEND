using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PacienteAlergia
{
    public class GetPacienteAlergiaByIdUseCase
    {
        private readonly IGenericRepository<PacienteAlergiaEntity> _repo;

        public GetPacienteAlergiaByIdUseCase(IGenericRepository<PacienteAlergiaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<PacienteAlergiaEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El detalle de la cita no existe.");
            return entity;
        }
    }
}