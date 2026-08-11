using Application.DTOs.Paciente;
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
            var paciente_alergia = await _repo.GetEntityByIdAsync(id);

            if (paciente_alergia == null)
            {
                throw new ArgumentException("No Existe");
            }

            paciente_alergia.PacienteId = dto.PacienteId;
            paciente_alergia.Sustancia = dto.Sustancia;
            paciente_alergia.Reaccion = dto.Reaccion;
            paciente_alergia.Severidad = dto.Severidad;
            paciente_alergia.Activo = dto.Activo;


            await _repo.UpdateAsync(paciente_alergia);

        }
    }
}