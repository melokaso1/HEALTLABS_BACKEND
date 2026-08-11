using Domain.Entities;
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

        var adminRol = roles.FirstOrDefault(r => r.NombreRol == "Administrador");
        var medicoRol = roles.FirstOrDefault(r => r.NombreRol == "Médico");
        var recepcionistaRol = roles.FirstOrDefault(r => r.NombreRol == "Recepcionista");
        var pacienteRol = roles.FirstOrDefault(r => r.NombreRol == "Paciente");

        var rolesPermisos = new List<RolPermisoEntity>();

        if (adminRol != null)
        {
            foreach (var permiso in permisos)
            {
                rolesPermisos.Add(new RolPermisoEntity(adminRol.Id, permiso.Id));
            }
        }

        if (medicoRol != null)
        {
            var codigosMedico = new[] { "citas:ver", "citas:modificar", "historias:ver", "historias:escribir", "pacientes:ver" };
            foreach (var p in permisos.Where(p => codigosMedico.Contains(p.Codigo)))
            {
                rolesPermisos.Add(new RolPermisoEntity(medicoRol.Id, p.Id));
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

        if (pacienteRol != null)
        {
            var codigosPaciente = new[] { "citas:crear", "citas:ver", "citas:cancelar", "historias:ver" };
            foreach (var p in permisos.Where(p => codigosPaciente.Contains(p.Codigo)))
            {
                rolesPermisos.Add(new RolPermisoEntity(pacienteRol.Id, p.Id));
            }
        }

        if (rolesPermisos.Any())
        {
            await context.RolesPermiso.AddRangeAsync(rolesPermisos);
            await context.SaveChangesAsync();
        }
    }
}
