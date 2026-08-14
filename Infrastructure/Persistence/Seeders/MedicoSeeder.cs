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

        var medico = new MedicoEntity(
            empleado.Id,
            "RM-000001",
            true);
        await context.Medicos.AddAsync(medico);
        await context.SaveChangesAsync();

        var medicinaGeneral = await context.Especialidades
            .FirstOrDefaultAsync(e => e.Nombre == "Medicina General");
        if (medicinaGeneral is null) return;

        var alreadyLinked = await context.MedicosEspecialidad
            .AnyAsync(me => me.MedicoId == medico.Id && me.EspecialidadId == medicinaGeneral.Id);
        if (alreadyLinked) return;

        await context.MedicosEspecialidad.AddAsync(
            new MedicoEspecialidadEntity(medico.Id, medicinaGeneral.Id, principal: true));
        await context.SaveChangesAsync();
    }
}
