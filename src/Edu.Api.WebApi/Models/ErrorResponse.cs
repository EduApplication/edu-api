using System.Text.Json;

namespace Edu.Api.WebApi.Models;

/// <summary>
/// Model for API error responses
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Gets or sets the error code
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the error message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets additional error details
    /// </summary>
    public object? Details { get; set; }

    /// <summary>
    /// Gets or sets the request path that caused the error
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the error occurred
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Converts the error response to a JSON string
    /// </summary>
    /// <returns>JSON representation of the error response</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}