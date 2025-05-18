using System.Diagnostics;

namespace Edu.Api.WebApi.Middleware;

/// <summary>
/// Middleware for logging HTTP request details and timing
/// </summary>
public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<RequestLoggingMiddleware> _logger = logger;

    /// <summary>
    /// Processes an HTTP request to log request information and timing
    /// </summary>
    /// <param name="context">The HTTP context for the request</param>
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestId = Guid.NewGuid().ToString();
        using (Serilog.Context.LogContext.PushProperty("RequestId", requestId))
        {
            try
            {
                _logger.LogInformation(
                    "HTTP {RequestMethod} {RequestPath} started - RequestId: {RequestId}",
                    context.Request.Method, context.Request.Path, requestId);
                var originalBodyStream = context.Response.Body;
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;
                await _next(context);
                stopwatch.Stop();
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
                _logger.LogInformation(
                    "HTTP {RequestMethod} {RequestPath} finished in {ElapsedMilliseconds}ms with status {StatusCode} - RequestId: {RequestId}",
                    context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds,
                    context.Response.StatusCode, requestId);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex,
                    "HTTP {RequestMethod} {RequestPath} failed in {ElapsedMilliseconds}ms - RequestId: {RequestId}",
                    context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds, requestId);
                throw;
            }
        }
    }
}

/// <summary>
/// Extension methods for the RequestLoggingMiddleware
/// </summary>
public static class RequestLoggingMiddlewareExtensions
{
    /// <summary>
    /// Adds the request logging middleware to the application pipeline
    /// </summary>
    /// <param name="builder">The application builder</param>
    /// <returns>The application builder for chaining</returns>
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}