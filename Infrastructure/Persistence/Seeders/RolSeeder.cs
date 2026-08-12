using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class RolSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        // DBs seeded before the rename may still have "Médico"; align claim with AppRoles.Medico = "Profesional".
        var medicoLegacy = await context.Roles.FirstOrDefaultAsync(r => r.NombreRol == "Médico");
        if (medicoLegacy is not null)
        {
            medicoLegacy.Update(
                RolValueObject.Profesional,
                medicoLegacy.Descripcion ?? "Gestión de atenciones médicas e historias clínicas",
                medicoLegacy.Activo);
            await context.SaveChangesAsync();
        }

        if (await context.Roles.AnyAsync()) return;

        var roles = new List<RolEntity>
        {
            new(RolValueObject.Administrador, "Acceso total al sistema y gestión de configuración", true),
            new(RolValueObject.Profesional, "Gestión de atenciones médicas e historias clínicas", true),
            new(RolValueObject.Recepcionista, "Gestión de citas y atención al paciente en recepción", true)
        };

        await context.Roles.AddRangeAsync(roles);
        await context.SaveChangesAsync();
    }
}
