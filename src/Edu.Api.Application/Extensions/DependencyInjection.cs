using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Edu.Api.Application.Mappings;

namespace Edu.Api.Application.Extensions;

/// <summary>
/// Extension methods for configuring application layer dependencies
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds application layer services to the dependency injection container
    /// </summary>
    /// <param name="services">The service collection to add services to</param>
    /// <returns>The service collection for method chaining</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        RegisterServices(services, assembly);
        RegisterMappers(services, assembly);
        return services;
    }

    /// <summary>
    /// Automatically registers all service interfaces and implementations in the assembly
    /// </summary>
    /// <param name="services">The service collection to add services to</param>
    /// <param name="assembly">The assembly containing service types</param>
    private static void RegisterServices(IServiceCollection services, Assembly assembly)
    {
        var serviceInterfaces = assembly.GetTypes()
            .Where(t => t.IsInterface && t.Name.EndsWith("Service"))
            .ToList();

        var serviceImplementations = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"))
            .ToList();

        foreach (var interfaceType in serviceInterfaces)
        {
            var implementationType = serviceImplementations.FirstOrDefault(
                t => interfaceType.IsAssignableFrom(t)
            );

            if (implementationType != null)
            {
                services.AddScoped(interfaceType, implementationType);
            }
        }
    }

    /// <summary>
    /// Automatically registers all mapper implementations with their interfaces
    /// </summary>
    /// <param name="services">The service collection to add mappers to</param>
    /// <param name="assembly">The assembly containing mapper types</param>
    private static void RegisterMappers(IServiceCollection services, Assembly assembly)
    {
        var mapperTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Mapper"))
            .ToList();

        foreach (var mapperType in mapperTypes)
        {
            var interfaceTypes = mapperType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapper<,,,,>))
                .ToList();

            foreach (var interfaceType in interfaceTypes)
            {
                services.AddScoped(interfaceType, mapperType);
            }
        }
    }
}