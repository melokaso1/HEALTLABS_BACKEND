using Application.DTOs.Diagnostico;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Diagnostico
{
    public class UpdateDiagnosticoUseCase
    {
        private readonly IGenericRepository<DiagnosticoEntity> _repo;

        public UpdateDiagnosticoUseCase(IGenericRepository<DiagnosticoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateDiagnosticoDto dto)
        {
            var diagnostico = await _repo.GetEntityByIdAsync(id);

            if (diagnostico == null)
            {
                throw new ArgumentException("No Existe");
            }

            diagnostico.CodigoCie10 = dto.CodigoCie10;
            diagnostico.Descripcion = dto.Descripcion;
            diagnostico.Activo = dto.Activo;

            await _repo.UpdateAsync(diagnostico);

        }
    }
}