using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class HorarioSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        if (await context.Horarios.AnyAsync()) return;

        var medico = await context.Medicos.FirstOrDefaultAsync();
        if (medico is null) return;

        await context.Horarios.AddAsync(new HorarioEntity(
            medico.Id,
            new TimeOnly(8, 0),
            new TimeOnly(17, 0),
            new TimeOnly(12, 0),
            new TimeOnly(13, 0)));
        await context.SaveChangesAsync();
    }
}
