using Application.DTOs.Paciente;
using Domain.Interfaces;

namespace Application.UseCases.Pacientes;

public sealed class BuscarPacientePorDocumentoUseCase
{
    private readonly IPacienteRepository _repo;

    public BuscarPacientePorDocumentoUseCase(IPacienteRepository repo) => _repo = repo;

    public async Task<BuscarPacientePorDocumentoResult> ExecuteAsync(Guid tipoDocumento, string numeroDocumento)
    {
        var paciente = await _repo.GetByDocumentoAsync(tipoDocumento, numeroDocumento);
        if (paciente is null)
            return BuscarPacientePorDocumentoResult.NoEncontrado();

        if (!paciente.Activo)
            return BuscarPacientePorDocumentoResult.Inactivo();

        return BuscarPacientePorDocumentoResult.Encontrado(PacienteResponseDto.FromEntity(paciente));
    }
}

public sealed class BuscarPacientePorDocumentoResult
{
    public PacienteResponseDto? Paciente { get; private init; }
    public bool NoExiste { get; private init; }
    public bool EstaInactivo { get; private init; }

    public static BuscarPacientePorDocumentoResult NoEncontrado() => new() { NoExiste = true };
    public static BuscarPacientePorDocumentoResult Inactivo() => new() { EstaInactivo = true };
    public static BuscarPacientePorDocumentoResult Encontrado(PacienteResponseDto paciente) => new() { Paciente = paciente };
}
