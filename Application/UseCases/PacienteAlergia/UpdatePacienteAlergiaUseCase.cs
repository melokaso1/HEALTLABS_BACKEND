using Application.DTOs.PacienteAlergia;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PacienteAlergia
{
    public class UpdatePacienteAlergiaUseCase
    {
        private readonly IGenericRepository<PacienteAlergiaEntity> _repo;

        public UpdatePacienteAlergiaUseCase(IGenericRepository<PacienteAlergiaEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdatePacienteAlergiaDto dto)
        {
            var paciente_alergia = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            paciente_alergia.Update(dto.Sustancia, dto.Reaccion, dto.Severidad, dto.Activo);
            await _repo.UpdateAsync(paciente_alergia);
        }
    }
}
