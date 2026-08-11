using Application.DTOs.Paciente;
using Application.DTOs.PacienteAlergia;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PacienteAlergia
{
    public class CreatePacienteAlergiaUseCase
    {
        private readonly IGenericRepository<PacienteAlergiaEntity> _repo;

        public CreatePacienteAlergiaUseCase(IGenericRepository<PacienteAlergiaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<PacienteAlergiaEntity> ExecuteAsync(CreatePacienteAlergiaDto dto)
        {
            var paciente_alergia = new PacienteAlergiaEntity(
                                        dto.PacienteId,
                                        dto.Sustancia,
                                        dto.Reaccion,
                                        dto.Severidad,
                                        dto.Activo
                                        );

            return await _repo.AddAsync(paciente_alergia);
        }
    }
}