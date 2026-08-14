using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class PacienteRepository : IPacienteRepository
{
    private readonly AppDbContext _context;

    public PacienteRepository(AppDbContext context) => _context = context;

    public async Task<PacienteEntity?> GetByIdWithPersonaAsync(Guid id)
    {
        return await WithPersona().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PacienteEntity?> GetByDocumentoAsync(Guid tipoDocumentoId, string numeroDocumento)
    {
        return await WithPersona()
            .FirstOrDefaultAsync(p => p.Persona!.TipoDocumentoId == tipoDocumentoId
                && p.Persona.NumeroDocumento == numeroDocumento);
    }

    public async Task<IReadOnlyList<PacienteEntity>> GetByNumeroDocumentoAsync(string numeroDocumento)
    {
        return await WithPersona()
            .Where(p => p.Persona!.NumeroDocumento == numeroDocumento)
            .ToListAsync();
    }

    public async Task<IEnumerable<PacienteEntity>> GetAllWithPersonaAsync()
    {
        return await WithPersona().ToListAsync();
    }

    private IQueryable<PacienteEntity> WithPersona()
    {
        return _context.Pacientes
            .AsNoTracking()
            .Include(p => p.Persona)
                .ThenInclude(persona => persona!.TipoDocumento)
            .Include(p => p.Persona)
                .ThenInclude(persona => persona!.Sexo)
            .Include(p => p.Persona)
                .ThenInclude(persona => persona!.Telefonos)
            .Include(p => p.Persona)
                .ThenInclude(persona => persona!.Direcciones);
    }
}
