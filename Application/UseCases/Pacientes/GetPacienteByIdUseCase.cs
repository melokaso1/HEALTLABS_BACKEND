using Application.DTOs.Paciente;
using Domain.Interfaces;

namespace Application.UseCases.Pacientes;

public sealed class GetPacienteByIdUseCase
{
    private readonly IPacienteRepository _repo;

    public GetPacienteByIdUseCase(IPacienteRepository repo) => _repo = repo;

    public async Task<PacienteResponseDto?> ExecuteAsync(Guid id)
    {
        var entity = await _repo.GetByIdWithPersonaAsync(id);
        return entity is null ? null : PacienteResponseDto.FromEntity(entity);
    }
}
