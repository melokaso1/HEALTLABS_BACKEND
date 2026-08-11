using Application.DTOs.Cita;
using Application.DTOs.Paciente;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Pacientes
{
    public class UpdatePacienteUseCase
    {
        private readonly IGenericRepository<PacienteEntity> _repo;

        public UpdatePacienteUseCase(IGenericRepository<PacienteEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdatePacienteDto dto)
        {
            var paciente = await _repo.GetEntityByIdAsync(id);

            if (paciente == null)
            {
                throw new ArgumentException("No Existe");
            }

            paciente.PersonaId = dto.PersonaId;
            paciente.Activo = dto.Activo;

            await _repo.UpdateAsync(paciente);

        }
    }
}