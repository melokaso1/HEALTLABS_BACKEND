using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class CitaSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        if (await context.Citas.AnyAsync()) return;

        var paciente = await context.Pacientes.FirstOrDefaultAsync();
        var medico = await context.Medicos.FirstOrDefaultAsync();
        var estado = await context.EstadosCita.FirstOrDefaultAsync(e => e.Codigo == "AGENDADA");
        var tipo = await context.TiposCita.FirstOrDefaultAsync(t => t.Codigo == "CG");
        var recepcionista = await context.Usuarios.FirstOrDefaultAsync(u => u.Username == "recepcion");

        if (paciente is null || medico is null || estado is null || tipo is null || recepcionista is null)
            return;

        var cita = new CitaEntity(
            paciente.Id,
            medico.Id,
            estado.Id,
            tipo.Id,
            GetNextBusinessDay(),
            new TimeOnly(9, 0),
            new TimeOnly(9, 30),
            "Consulta general de prueba",
            null,
            recepcionista.Id);

        await context.Citas.AddAsync(cita);
        await context.CitasHistorialEstado.AddAsync(new CitaHistorialEstadoEntity(
            cita.Id,
            estado.Id,
            estado.Id,
            recepcionista.Id,
            "Cita creada por datos semilla"));
        await context.SaveChangesAsync();
    }

    private static DateOnly GetNextBusinessDay()
    {
        var fecha = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(1);
        while (fecha.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            fecha = fecha.AddDays(1);

        return fecha;
    }
}
