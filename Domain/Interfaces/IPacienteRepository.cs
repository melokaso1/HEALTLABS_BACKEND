using Domain.Entities;

namespace Domain.Interfaces;

public interface IPacienteRepository
{
    Task<PacienteEntity?> GetByIdWithPersonaAsync(Guid id);
    Task<PacienteEntity?> GetByDocumentoAsync(Guid tipoDocumentoId, string numeroDocumento);
    Task<IReadOnlyList<PacienteEntity>> GetByNumeroDocumentoAsync(string numeroDocumento);
    Task<IEnumerable<PacienteEntity>> GetAllWithPersonaAsync();
}
