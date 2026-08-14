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
            // Skip orphans (Paciente without Persona) so one bad row does not fail the whole list.
            return pacientes
                .Where(p => p.Persona != null)
                .Select(PacienteResponseDto.FromEntity);
        }
    }
}
