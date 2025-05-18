using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Edu.Api.Infrastructure.Data;

/// <summary>
/// Factory for creating AppDbContext instances at design time
/// </summary>
/// <remarks>
/// This factory is used by Entity Framework Core tools like migrations
/// to create database contexts outside of the application's runtime
/// </remarks>
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    /// <summary>
    /// Creates a new instance of AppDbContext
    /// </summary>
    /// <param name="args">Command line arguments</param>
    /// <returns>A configured AppDbContext instance</returns>
    public AppDbContext CreateDbContext(string[] args)
    {
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables();
        var config = configBuilder.Build();
        var connectionString = config.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        return new AppDbContext(optionsBuilder.Options);
    }
}