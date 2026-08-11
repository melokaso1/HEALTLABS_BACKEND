using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public class TipoDocumentoSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        if (await context.TiposDocumento.AnyAsync()) return;

        var tiposDocumento = new List<TipoDocumentoEntity>
        {
            new("CC", "Cédula de Ciudadanía"),
            new("TI", "Tarjeta de Identidad"),
            new("CE", "Cédula de Extranjería"),
            new("PAS", "Pasaporte")
        };

        await context.TiposDocumento.AddRangeAsync(tiposDocumento);
        await context.SaveChangesAsync();
    }
}
