using Domain.Interfaces;
using Application.DTOs.Paciente;

namespace Application.UseCases.Pacientes
{
    public class GetAllPacienteUseCase
    {
        private readonly IPacienteRepository _repo;

        public GetAllPacienteUseCase(IPacienteRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PacienteResponseDto>> ExecuteAsync()
        {
            var pacientes = await _repo.GetAllWithPersonaAsync();
            return pacientes.Select(PacienteResponseDto.FromEntity);
        }
    }
}
