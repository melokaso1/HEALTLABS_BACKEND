using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class PermisoSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        if (await context.Permisos.AnyAsync()) return;

        var permisos = new List<PermisoEntity>
        {
            // Módulo Citas
            new("citas:crear", "Citas", "Permiso para agendar nuevas citas médicas"),
            new("citas:ver", "Citas", "Permiso para visualizar la agenda de citas"),
            new("citas:modificar", "Citas", "Permiso para modificar o reprogramar citas"),
            new("citas:cancelar", "Citas", "Permiso para cancelar citas registradas"),

            // Módulo Historias Clínicas
            new("historias:ver", "HistoriasClinicas", "Permiso para consultar historias clínicas"),
            new("historias:escribir", "HistoriasClinicas", "Permiso para registrar atenciones y observaciones en la historia clínica"),

            // Módulo Usuarios y Configuración
            new("usuarios:ver", "Usuarios", "Permiso para ver la lista de usuarios"),
            new("usuarios:crear", "Usuarios", "Permiso para crear nuevos usuarios"),
            new("usuarios:modificar", "Usuarios", "Permiso para editar usuarios"),
            new("usuarios:eliminar", "Usuarios", "Permiso para inactivar o eliminar usuarios"),

            // Módulo Pacientes
            new("pacientes:ver", "Pacientes", "Permiso para consultar datos de pacientes"),
            new("pacientes:crear", "Pacientes", "Permiso para registrar nuevos pacientes"),
            new("pacientes:modificar", "Pacientes", "Permiso para actualizar datos de pacientes")
        };

        await context.Permisos.AddRangeAsync(permisos);
        await context.SaveChangesAsync();
    }
}
