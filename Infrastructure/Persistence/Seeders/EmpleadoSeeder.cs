using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class EmpleadoSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        if (await context.Empleados.AnyAsync()) return;

        var personas = await context.Personas.ToListAsync();
        var cargos = await context.Cargos.ToListAsync();

        if (!personas.Any() || !cargos.Any()) return;

        var personaAdmin = personas.FirstOrDefault(p => p.NumeroDocumento == "1000000001");
        var personaMedico = personas.FirstOrDefault(p => p.NumeroDocumento == "1000000002");
        var personaRecep = personas.FirstOrDefault(p => p.NumeroDocumento == "1000000003");

        var cargoAdmin = cargos.FirstOrDefault(c => c.Codigo == "ADM");
        var cargoMedico = cargos.FirstOrDefault(c => c.Codigo == "MED");
        var cargoRecep = cargos.FirstOrDefault(c => c.Codigo == "REC");

        var empleados = new List<EmpleadoEntity>();

        if (personaAdmin != null && cargoAdmin != null)
        {
            empleados.Add(new EmpleadoEntity(personaAdmin.Id, cargoAdmin.Id, new DateOnly(2023, 1, 1), null, true));
        }

        if (personaMedico != null && cargoMedico != null)
        {
            empleados.Add(new EmpleadoEntity(personaMedico.Id, cargoMedico.Id, new DateOnly(2023, 2, 1), null, true));
        }

        if (personaRecep != null && cargoRecep != null)
        {
            empleados.Add(new EmpleadoEntity(personaRecep.Id, cargoRecep.Id, new DateOnly(2023, 3, 1), null, true));
        }

        if (empleados.Any())
        {
            await context.Empleados.AddRangeAsync(empleados);
            await context.SaveChangesAsync();
        }
    }
}
