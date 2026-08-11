using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Seeders;

public static class SeederDependencyInjection
{
    public static IServiceCollection AddPersistenceSeeders(this IServiceCollection services)
    {
        services.AddScoped<SexoSeeder>();
        services.AddScoped<TipoDocumentoSeeder>();
        services.AddScoped<RolSeeder>();
        services.AddScoped<PermisoSeeder>();
        services.AddScoped<RolPermisoSeeder>();
        services.AddScoped<CargoSeeder>();
        services.AddScoped<PersonaSeeder>();
        services.AddScoped<EmpleadoSeeder>();
        services.AddScoped<UsuarioSeeder>();
        services.AddScoped<EstadoCitaSeeder>();
        services.AddScoped<TipoCitaSeeder>();
        services.AddScoped<IDbSeeder, DbSeeder>();

        return services;
    }
}
