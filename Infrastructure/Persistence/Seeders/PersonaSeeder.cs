using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class PersonaSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        if (await context.Personas.AnyAsync()) return;

        var tipoCc = await context.TiposDocumento.FirstOrDefaultAsync(t => t.Codigo == "CC");
        var sexoM = await context.Sexos.FirstOrDefaultAsync(s => s.Codigo == "M");
        var sexoF = await context.Sexos.FirstOrDefaultAsync(s => s.Codigo == "F");

        if (tipoCc == null) return;

        var personas = new List<PersonaEntity>
        {
            new(
                "Carlos",
                "Mendoza",
                tipoCc.Id,
                "1000000001",
                new DateOnly(1985, 5, 15),
                sexoM?.Id
            ),
            new(
                "Laura",
                "Gómez",
                tipoCc.Id,
                "1000000002",
                new DateOnly(1990, 8, 20),
                sexoF?.Id
            ),
            new(
                "Ana",
                "Martínez",
                tipoCc.Id,
                "1000000003",
                new DateOnly(1995, 3, 10),
                sexoF?.Id
            )
        };

        await context.Personas.AddRangeAsync(personas);
        await context.SaveChangesAsync();
    }
}
