using Domain.Entities;

namespace Domain.Interfaces;

public interface IPacienteRepository
{
    Task<PacienteEntity?> GetByDocumentoAsync(Guid tipoDocumentoId, string numeroDocumento);
    Task<IEnumerable<PacienteEntity>> GetAllWithPersonaAsync();
}
