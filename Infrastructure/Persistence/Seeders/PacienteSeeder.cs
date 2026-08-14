using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class PacienteSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        if (await context.Pacientes.AnyAsync()) return;

        var persona = await context.Personas
            .FirstOrDefaultAsync(p => p.NumeroDocumento == "1000000004");

        if (persona is null) return;

        await context.Pacientes.AddAsync(new PacienteEntity(persona.Id, true, "O+"));
        await context.SaveChangesAsync();
    }
}
