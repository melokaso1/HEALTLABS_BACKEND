using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class MedicoSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        if (await context.Medicos.AnyAsync()) return;

        var empleado = await context.Empleados
            .Include(e => e.Persona)
            .FirstOrDefaultAsync(e => e.Persona!.NumeroDocumento == "1000000002");

        if (empleado is null) return;

        await context.Medicos.AddAsync(new MedicoEntity(
            empleado.Id,
            "RM-000001",
            true));
        await context.SaveChangesAsync();
    }
}
