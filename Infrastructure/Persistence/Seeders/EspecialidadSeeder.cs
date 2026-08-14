using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class EspecialidadSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        if (await context.Especialidades.AnyAsync()) return;

        var especialidades = new List<EspecialidadEntity>
        {
            new("Medicina General", "Atención primaria y consulta general"),
            new("Cardiología", "Enfermedades del corazón y sistema cardiovascular"),
            new("Pediatría", "Atención médica de niños y adolescentes"),
            new("Ginecología", "Salud reproductiva y del aparato genital femenino"),
            new("Dermatología", "Enfermedades de la piel, cabello y uñas"),
            new("Ortopedia", "Sistema musculoesquelético y traumatología"),
        };

        await context.Especialidades.AddRangeAsync(especialidades);
        await context.SaveChangesAsync();
    }
}
