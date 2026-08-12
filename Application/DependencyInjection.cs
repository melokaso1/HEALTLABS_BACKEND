using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        var useCaseTypes = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false, IsPublic: true })
            .Where(t =>
                t.Name.EndsWith("UseCase", StringComparison.Ordinal) ||
                t.Name is "GetAntecedentesByType" or "GetPersonaDireccionByPersonId");

        foreach (var type in useCaseTypes)
            services.AddScoped(type);

        services.AddScoped<UseCases.Citas.CitaSchedulingRules>();

        return services;
    }
}
