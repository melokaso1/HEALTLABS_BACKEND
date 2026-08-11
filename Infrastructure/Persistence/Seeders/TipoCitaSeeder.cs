using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class TipoCitaSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        if (await context.TiposCita.AnyAsync()) return;

        var tiposCita = new List<TipoCitaEntity>
        {
            new("CG", "Consulta General", 30, true),
            new("CTL", "Control", 20, true),
            new("ESP", "Especialidad", 45, true),
            new("LEX", "Lectura de Exámenes", 15, true)
        };

        await context.TiposCita.AddRangeAsync(tiposCita);
        await context.SaveChangesAsync();
    }
}
