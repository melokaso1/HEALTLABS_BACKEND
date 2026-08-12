using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class RolPermisoSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        if (await context.RolesPermiso.AnyAsync()) return;

        var roles = await context.Roles.ToListAsync();
        var permisos = await context.Permisos.ToListAsync();

        if (!roles.Any() || !permisos.Any()) return;

        var adminRol = roles.FirstOrDefault(r => r.NombreRol == RolValueObject.Administrador);
        var profesionalRol = roles.FirstOrDefault(r => r.NombreRol == RolValueObject.Profesional);
        var recepcionistaRol = roles.FirstOrDefault(r => r.NombreRol == RolValueObject.Recepcionista);

        var rolesPermisos = new List<RolPermisoEntity>();

        if (adminRol != null)
        {
            foreach (var permiso in permisos)
            {
                rolesPermisos.Add(new RolPermisoEntity(adminRol.Id, permiso.Id));
            }
        }

        if (profesionalRol != null)
        {
            var codigosProfesional = new[] { "citas:ver", "citas:modificar", "historias:ver", "historias:escribir", "pacientes:ver" };
            foreach (var p in permisos.Where(p => codigosProfesional.Contains(p.Codigo)))
            {
                rolesPermisos.Add(new RolPermisoEntity(profesionalRol.Id, p.Id));
            }
        }

        if (recepcionistaRol != null)
        {
            var codigosRecep = new[] { "citas:crear", "citas:ver", "citas:modificar", "citas:cancelar", "pacientes:ver", "pacientes:crear", "pacientes:modificar" };
            foreach (var p in permisos.Where(p => codigosRecep.Contains(p.Codigo)))
            {
                rolesPermisos.Add(new RolPermisoEntity(recepcionistaRol.Id, p.Id));
            }
        }

        if (rolesPermisos.Any())
        {
            await context.RolesPermiso.AddRangeAsync(rolesPermisos);
            await context.SaveChangesAsync();
        }
    }
}
