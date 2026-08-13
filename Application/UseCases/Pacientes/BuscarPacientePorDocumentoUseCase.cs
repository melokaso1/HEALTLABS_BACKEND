using Application.DTOs.Paciente;
using Domain.Interfaces;

namespace Application.UseCases.Pacientes;

public sealed class BuscarPacientePorDocumentoUseCase
{
    private readonly IPacienteRepository _repo;

    public BuscarPacientePorDocumentoUseCase(IPacienteRepository repo) => _repo = repo;

    public async Task<BuscarPacientePorDocumentoResult> ExecuteAsync(Guid tipoDocumento, string? numeroDocumento)
    {
        var numero = numeroDocumento?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(numero))
            return BuscarPacientePorDocumentoResult.DocumentoVacio();

        var paciente = await _repo.GetByDocumentoAsync(tipoDocumento, numero);
        if (paciente is null)
            return BuscarPacientePorDocumentoResult.NoEncontrado();

        if (!paciente.Activo)
            return BuscarPacientePorDocumentoResult.Inactivo();

        return BuscarPacientePorDocumentoResult.Encontrado(PacienteResponseDto.FromEntity(paciente));
    }

    public async Task<BuscarPacientePorDocumentoResult> ExecutePorNumeroAsync(string? numeroDocumento)
    {
        var numero = numeroDocumento?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(numero))
            return BuscarPacientePorDocumentoResult.DocumentoVacio();

        var pacientes = await _repo.GetByNumeroDocumentoAsync(numero);
        if (pacientes.Count == 0)
            return BuscarPacientePorDocumentoResult.NoEncontrado();

        if (pacientes.Count > 1)
            return BuscarPacientePorDocumentoResult.Ambiguo();

        var paciente = pacientes[0];
        if (!paciente.Activo)
            return BuscarPacientePorDocumentoResult.Inactivo();

        return BuscarPacientePorDocumentoResult.Encontrado(PacienteResponseDto.FromEntity(paciente));
    }
}

public sealed class BuscarPacientePorDocumentoResult
{
    public PacienteResponseDto? Paciente { get; private init; }
    public bool NumeroVacio { get; private init; }
    public bool NoExiste { get; private init; }
    public bool EstaInactivo { get; private init; }
    public bool EstaAmbiguo { get; private init; }

    public static BuscarPacientePorDocumentoResult DocumentoVacio() => new() { NumeroVacio = true };
    public static BuscarPacientePorDocumentoResult NoEncontrado() => new() { NoExiste = true };
    public static BuscarPacientePorDocumentoResult Inactivo() => new() { EstaInactivo = true };
    public static BuscarPacientePorDocumentoResult Ambiguo() => new() { EstaAmbiguo = true };
    public static BuscarPacientePorDocumentoResult Encontrado(PacienteResponseDto paciente) => new() { Paciente = paciente };
}
