using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class RolSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        if (await context.Roles.AnyAsync()) return;

        var roles = new List<RolEntity>
        {
            new("Administrador", "Acceso total al sistema y gestión de configuración", true),
            new("Médico", "Gestión de atenciones médicas e historias clínicas", true),
            new("Recepcionista", "Gestión de citas y atención al paciente en recepción", true)
        };

        await context.Roles.AddRangeAsync(roles);
        await context.SaveChangesAsync();
    }
}
