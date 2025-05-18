using System.Diagnostics;
using Prometheus;

namespace Edu.Api.WebApi.Middleware;

/// <summary>
/// Middleware for collecting and recording application metrics
/// </summary>
public class MetricsMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    private static readonly HashSet<string> _excludedPaths = new(StringComparer.OrdinalIgnoreCase)
        {
            "/metrics",
            "/health",
            "/swagger",
            "/favicon.ico"
        };


    private static readonly Counter HttpRequests = Metrics.CreateCounter(
        "api_requests_total",
        "Total number of HTTP requests",
        new CounterConfiguration
        {
            LabelNames = ["controller", "action", "method", "status_code"]
        });

    private static readonly Histogram HttpRequestDuration = Metrics.CreateHistogram(
        "api_request_duration_seconds",
        "Duration of HTTP requests",
        new HistogramConfiguration
        {
            LabelNames = ["controller", "action"]
        });

    private static readonly Counter EntityOperations = Metrics.CreateCounter(
        "api_entity_operations_total",
        "Total number of entity operations",
        new CounterConfiguration
        {
            LabelNames = ["controller", "operation", "status"]
        });

    /// <summary>
    /// Processes an HTTP request to record metrics about the request
    /// </summary>
    /// <param name="context">The HTTP context for the request</param>
    public async Task InvokeAsync(HttpContext context)
    {
        string path = context.Request.Path.Value ?? "";
        bool isExcluded = false;

        foreach (var excludedPath in _excludedPaths)
        {
            if (path.StartsWith(excludedPath, StringComparison.OrdinalIgnoreCase))
            {
                isExcluded = true;
                break;
            }
        }

        if (isExcluded)
        {
            await _next(context);
            return;
        }

        var routeData = context.GetRouteData();
        string controllerName = "unknown";
        string actionName = "unknown";
        string operation = "unknown";

        if (routeData?.Values != null)
        {
            if (routeData.Values.TryGetValue("controller", out var controller))
            {
                controllerName = controller?.ToString() ?? "unknown";
            }

            if (routeData.Values.TryGetValue("action", out var action))
            {
                actionName = action?.ToString() ?? "unknown";
            }

            operation = DetermineOperationType(context.Request.Method, actionName);
        }

        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);

            string statusCode = context.Response.StatusCode.ToString();

            HttpRequests
                .WithLabels(controllerName, actionName, context.Request.Method, statusCode)
                .Inc();

            if (operation != "unknown" && controllerName != "unknown")
            {
                EntityOperations
                    .WithLabels(controllerName, operation, statusCode)
                    .Inc();
            }
        }
        catch (Exception)
        {
            const string exceptionStatusCode = "500";

            HttpRequests
                .WithLabels(controllerName, actionName, context.Request.Method, exceptionStatusCode)
                .Inc();

            if (operation != "unknown" && controllerName != "unknown")
            {
                EntityOperations
                    .WithLabels(controllerName, operation, exceptionStatusCode)
                    .Inc();
            }

            throw;
        }
        finally
        {
            stopwatch.Stop();
            HttpRequestDuration
                .WithLabels(controllerName, actionName)
                .Observe(stopwatch.Elapsed.TotalSeconds);
        }
    }

    /// <summary>
    /// Determines the operation type based on HTTP method and action name
    /// </summary>
    /// <param name="httpMethod">The HTTP method of the request</param>
    /// <param name="actionName">The action name from the route</param>
    /// <returns>A string representing the operation type</returns>
    private static string DetermineOperationType(string httpMethod, string actionName)
    {
        if (actionName.Equals("Create", StringComparison.OrdinalIgnoreCase))
            return "create";

        if (actionName.Equals("Update", StringComparison.OrdinalIgnoreCase))
            return "update";

        if (actionName.Equals("Delete", StringComparison.OrdinalIgnoreCase))
            return "delete";

        if (actionName.Equals("GetAll", StringComparison.OrdinalIgnoreCase))
            return "get_all";

        if (actionName.Equals("GetById", StringComparison.OrdinalIgnoreCase))
            return "get_by_id";

        return httpMethod.ToUpper() switch
        {
            "GET" => "read",
            "POST" => "create",
            "PUT" or "PATCH" => "update",
            "DELETE" => "delete",
            _ => "unknown",
        };
    }
}