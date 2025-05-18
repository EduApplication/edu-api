using Edu.Api.Domain.Exceptions;
using Edu.Api.WebApi.Models;
using System.Net;
using System.Text.Json;

namespace Edu.Api.WebApi.Middleware;

/// <summary>
/// Middleware for global exception handling
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    /// <summary>
    /// Initializes a new instance of the error handling middleware
    /// </summary>
    /// <param name="next">The next middleware in the pipeline</param>
    /// <param name="logger">The logger</param>
    /// <param name="environment">The web host environment</param>
    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger,
        IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    /// <summary>
    /// Processes an HTTP request to handle exceptions
    /// </summary>
    /// <param name="context">The HTTP context</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var error = CreateErrorResponse(context, exception);
        var statusCode = GetStatusCode(exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        _logger.LogError(exception, "An unhandled exception occurred: {Message}. Path: {Path}",
            exception.Message, context.Request.Path);

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var json = JsonSerializer.Serialize(error, options);

        await context.Response.WriteAsync(json);
    }

    private ErrorResponse CreateErrorResponse(HttpContext context, Exception exception)
    {
        var path = context.Request.Path;

        if (exception is AppException appException)
        {
            return new ErrorResponse
            {
                Code = appException.Code,
                Message = appException.Message,
                Details = appException.Details,
                Path = path,
                Timestamp = DateTime.UtcNow
            };
        }

        // For system or unexpected exceptions
        return new ErrorResponse
        {
            Code = "internal_server_error",
            Message = _environment.IsDevelopment()
                ? exception.Message
                : "An unexpected error occurred",
            Details = _environment.IsDevelopment()
                ? new
                {
                    StackTrace = exception.StackTrace,
                    Source = exception.Source,
                    InnerException = exception.InnerException?.Message
                }
                : null,
            Path = path,
            Timestamp = DateTime.UtcNow
        };
    }

    private int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            AppException appException => appException.StatusCode,
            ArgumentException => (int)HttpStatusCode.BadRequest,
            InvalidOperationException => (int)HttpStatusCode.BadRequest,
            UnauthorizedAccessException => (int)HttpStatusCode.Forbidden,
            _ => (int)HttpStatusCode.InternalServerError
        };
    }
}

/// <summary>
/// Extension methods for the error handling middleware
/// </summary>
public static class ErrorHandlingMiddlewareExtensions
{
    /// <summary>
    /// Adds the global error handling middleware to the application pipeline
    /// </summary>
    /// <param name="builder">The application builder</param>
    /// <returns>The application builder for chaining</returns>
    public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}