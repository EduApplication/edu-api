using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace Edu.Api.Infrastructure.Logging;

/// <summary>
/// Static class for configuring application logging
/// </summary>
public static class LoggingConfiguration
{
    /// <summary>
    /// Configures Serilog with console and Elasticsearch sinks
    /// </summary>
    /// <param name="configuration">The application configuration</param>
    /// <returns>A configured Serilog logger configuration</returns>
    public static LoggerConfiguration ConfigureLogger(IConfiguration configuration)
    {
        return new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("EduApi", LogEventLevel.Information)
            .WriteTo.Console()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"] ?? ""))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"edu-api-{DateTime.UtcNow:yyyy.MM}",
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                ModifyConnectionSettings = conn => conn
                    .ServerCertificateValidationCallback((o, certificate, chain, errors) => true),
                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                EmitEventFailureHandling.RaiseCallback,
                FailureCallback = e => Console.WriteLine($"Error log in Elasticsearch: {e.Exception?.Message}")
            });
    }
}