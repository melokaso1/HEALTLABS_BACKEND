using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Serialization;

/// <summary>Documents authorization roles and the controller action's purpose in Swagger.</summary>
public sealed class AuthorizeOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var action = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
        if (action is null)
            return;

        var hasAllowAnonymous = action.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
            || action.ControllerTypeInfo.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);
        if (hasAllowAnonymous)
        {
            AppendDescription(operation, "Acceso: público. Propósito: autenticación o recuperación de acceso.");
            return;
        }

        var authorizations = action.MethodInfo.GetCustomAttributes<AuthorizeAttribute>(inherit: true)
            .Concat(action.ControllerTypeInfo.GetCustomAttributes<AuthorizeAttribute>(inherit: true))
            .ToArray();
        var roles = authorizations
            .Select(attribute => attribute.Roles)
            .Where(roles => !string.IsNullOrWhiteSpace(roles))
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        var access = roles.Length > 0
            ? $"Acceso: roles {string.Join(" / ", roles)}."
            : authorizations.Length > 0
                ? "Acceso: usuario autenticado."
                : "Acceso: público.";
        AppendDescription(operation, $"{access} Propósito: {action.ControllerTypeInfo.Name.Replace("Controller", string.Empty)}.{action.MethodInfo.Name}.");
    }

    private static void AppendDescription(OpenApiOperation operation, string description)
    {
        operation.Description = string.IsNullOrWhiteSpace(operation.Description)
            ? description
            : $"{operation.Description}<br/>{description}";
    }
}
