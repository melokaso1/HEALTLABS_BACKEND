using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class SexoSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        if (await context.Sexos.AnyAsync()) return;

        var sexos = new List<SexoEntity>
        {
            new("M", "Masculino"),
            new("F", "Femenino"),
            new("O", "Otro/No especificado")
        };

        await context.Sexos.AddRangeAsync(sexos);
        await context.SaveChangesAsync();
    }
}
