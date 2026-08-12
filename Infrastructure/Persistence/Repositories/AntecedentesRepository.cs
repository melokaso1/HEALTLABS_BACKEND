using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class AntecedentesRepository : GenericRepository<AntecedenteEntity>, IAntecedentesRepository
{
    public AntecedentesRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<AntecedenteEntity>> GetEntityByDateAsync(DateOnly fecha)
    {
        return await DbSet.AsNoTracking()
            .Where(a => DateOnly.FromDateTime(a.FechaRegistro) == fecha)
            .ToListAsync();
    }

    public async Task<IEnumerable<AntecedenteEntity>> GetEntityByType(string tipo)
    {
        return await DbSet.AsNoTracking()
            .Where(a => a.Tipo == tipo)
            .ToListAsync();
    }
}
