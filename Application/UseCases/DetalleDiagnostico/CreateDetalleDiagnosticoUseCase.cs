using Application.DTOs.DetalleDiagnostico;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.DetalleDiagnostico
{
    public class CreateDetalleDiagnosticoUseCase
    {
        private readonly IGenericRepository<DetalleDiagnosticoEntity> _repo;

        public CreateDetalleDiagnosticoUseCase(IGenericRepository<DetalleDiagnosticoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<DetalleDiagnosticoEntity> ExecuteAsync(CreateDetalleDiagnosticoDto dto)
        {
            var entity = new DetalleDiagnosticoEntity(
                dto.DetalleCitaId,
                dto.DiagnosticoId,
                dto.Principal);

            return await _repo.AddAsync(entity);
        }
    }
}
