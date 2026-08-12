using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class PersonaDireccionRepository : GenericRepository<PersonaDireccionEntity>, IPersonaDireccionRepository
{
    public PersonaDireccionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<PersonaDireccionEntity>> GetEntityByPersonIdAsync(Guid personaId)
    {
        return await DbSet.AsNoTracking()
            .Where(d => d.PersonaId == personaId)
            .ToListAsync();
    }
}
