using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class CargoSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        if (await context.Cargos.AnyAsync()) return;

        var cargos = new List<CargoEntity>
        {
            new("ADM", "Administrador de Sistema", "Gestión total de la infraestructura y usuarios", 1),
            new("MED", "Médico General", "Atención a pacientes y consulta externa", 2),
            new("REC", "Recepcionista", "Atención al cliente y asignación de citas", 3)
        };

        await context.Cargos.AddRangeAsync(cargos);
        await context.SaveChangesAsync();
    }
}
