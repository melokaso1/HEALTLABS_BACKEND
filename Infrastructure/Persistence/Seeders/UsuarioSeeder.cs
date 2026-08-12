using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class UsuarioSeeder(AppDbContext context, IPasswordHasher passwordHasher)
{
    public async Task SeedAsync()
    {
        if (await context.Usuarios.AnyAsync()) return;

        var empleados = await context.Empleados.Include(e => e.Persona).ToListAsync();
        var roles = await context.Roles.ToListAsync();

        if (!empleados.Any() || !roles.Any()) return;

        var rolAdmin = roles.FirstOrDefault(r => r.NombreRol == RolValueObject.Administrador);
        var rolProfesional = roles.FirstOrDefault(r => r.NombreRol == RolValueObject.Profesional);
        var rolRecep = roles.FirstOrDefault(r => r.NombreRol == RolValueObject.Recepcionista);

        var empAdmin = empleados.FirstOrDefault(e => e.Persona?.NumeroDocumento == "1000000001");
        var empMedico = empleados.FirstOrDefault(e => e.Persona?.NumeroDocumento == "1000000002");
        var empRecep = empleados.FirstOrDefault(e => e.Persona?.NumeroDocumento == "1000000003");

        var usuarios = new List<UsuarioEntity>();

        if (empAdmin != null && rolAdmin != null)
        {
            usuarios.Add(new UsuarioEntity(
                empAdmin.Id,
                rolAdmin.Id,
                "admin",
                "admin@healtlabs.com",
                passwordHasher.Hash("Admin123!"),
                true,
                null,
                0,
                null,
                false,
                DateTime.UtcNow,
                1
            ));
        }

        if (empMedico != null && rolProfesional != null)
        {
            usuarios.Add(new UsuarioEntity(
                empMedico.Id,
                rolProfesional.Id,
                "medico1",
                "medico@healtlabs.com",
                passwordHasher.Hash("Medico123!"),
                true,
                null,
                0,
                null,
                false,
                DateTime.UtcNow,
                1
            ));
        }

        if (empRecep != null && rolRecep != null)
        {
            usuarios.Add(new UsuarioEntity(
                empRecep.Id,
                rolRecep.Id,
                "recepcion",
                "recepcion@healtlabs.com",
                passwordHasher.Hash("Recepcion123!"),
                true,
                null,
                0,
                null,
                false,
                DateTime.UtcNow,
                1
            ));
        }

        if (usuarios.Any())
        {
            await context.Usuarios.AddRangeAsync(usuarios);
            await context.SaveChangesAsync();
        }
    }
}
