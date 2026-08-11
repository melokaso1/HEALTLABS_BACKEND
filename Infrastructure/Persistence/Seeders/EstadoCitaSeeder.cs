using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class EstadoCitaSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        if (await context.EstadosCita.AnyAsync()) return;

        var estados = new List<EstadoCitaEntity>
        {
            new("AGENDADA", "Agendada"),
            new("CONFIRMADA", "Confirmada"),
            new("EN_SALA", "En Sala"),
            new("ATENDIDA", "Atendida"),
            new("CANCELADA", "Cancelada"),
            new("NO_ASISTIO", "No asistió")
        };

        await context.EstadosCita.AddRangeAsync(estados);
        await context.SaveChangesAsync();
    }
}
