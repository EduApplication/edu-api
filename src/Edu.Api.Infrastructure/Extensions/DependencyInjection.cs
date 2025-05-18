using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Edu.Api.Infrastructure.Data;
using Edu.Api.Infrastructure.Identity;

namespace Edu.Api.Infrastructure.Extensions;

/// <summary>
/// Extension methods for configuring infrastructure services
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds infrastructure services to the service collection
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configuration">The application configuration</param>
    /// <returns>The same service collection for chaining</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<Edu.Api.Domain.Common.ICurrentUser>(provider =>
            provider.GetRequiredService<IUserContext>());
        var assembly = Assembly.GetExecutingAssembly();
        RegisterRepositories(services, assembly);
        return services;
    }

    /// <summary>
    /// Registers all repository implementations from the assembly
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="assembly">The assembly containing repository implementations</param>
    /// <remarks>
    /// This method uses reflection to automatically find and register all repository interfaces
    /// and their implementations, eliminating the need for manual registration of each repository
    /// </remarks>
    private static void RegisterRepositories(IServiceCollection services, Assembly assembly)
    {
        var repositoryInterfaces = assembly.GetTypes()
            .Where(t => t.IsInterface && t.Name.EndsWith("Repository"))
            .ToList();
        var applicationAssembly = typeof(Edu.Api.Application.Extensions.DependencyInjection).Assembly;
        repositoryInterfaces.AddRange(applicationAssembly.GetTypes()
            .Where(t => t.IsInterface && t.Name.EndsWith("Repository")));
        var repositoryImplementations = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"))
            .ToList();
        foreach (var interfaceType in repositoryInterfaces.Distinct())
        {
            var implementationType = repositoryImplementations.FirstOrDefault(
                t => interfaceType.IsAssignableFrom(t)
            );
            if (implementationType != null)
            {
                services.AddScoped(interfaceType, implementationType);
            }
        }
    }
}