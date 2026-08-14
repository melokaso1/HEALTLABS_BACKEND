using Domain.Interfaces;
using Infrastructure.Auth;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Cap client-side Npgsql pool so a single API process cannot exhaust
    /// Supabase Session pooler slots (free tier often pool_size ≈ 15).
    /// Prefer Transaction pooler (port 6543) when possible.
    /// </summary>
    private const int DefaultMaxPoolSize = 10;
    private const int DefaultConnectionTimeoutSeconds = 15;

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("ConnectionStrings:DefaultConnection no está configurada.");

        connectionString = NormalizeNpgsqlConnectionString(connectionString);

        // Scoped (default): one AppDbContext per request/scope — never register as Singleton.
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString, npgsql =>
                npgsql.EnableRetryOnFailure(
                    maxRetryCount: 2,
                    maxRetryDelay: TimeSpan.FromSeconds(2),
                    errorCodesToAdd: null)));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<IAntecedentesRepository, AntecedentesRepository>();
        services.AddScoped<IPersonaDireccionRepository, PersonaDireccionRepository>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddPersistenceSeeders();

        return services;
    }

    internal static string NormalizeNpgsqlConnectionString(string connectionString)
    {
        var builder = new NpgsqlConnectionStringBuilder(connectionString)
        {
            Pooling = true
        };

        // Npgsql default MaxPoolSize is 100, which exceeds Supabase Session mode limits.
        if (builder.MaxPoolSize <= 0 || builder.MaxPoolSize > DefaultMaxPoolSize)
            builder.MaxPoolSize = DefaultMaxPoolSize;

        if (builder.Timeout <= 0)
            builder.Timeout = DefaultConnectionTimeoutSeconds;

        // Supabase Transaction pooler (PgBouncer, typically port 6543) does not support prepared statements.
        if (builder.Port == 6543)
            builder.MaxAutoPrepare = 0;

        return builder.ConnectionString;
    }
}
