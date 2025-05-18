namespace Edu.Api.Domain.Exceptions;

/// <summary>
/// Base exception for all application-specific exceptions
/// </summary>
public abstract class AppException : Exception
{
    /// <summary>
    /// Gets the error code for this exception
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Gets the HTTP status code that should be returned to the client
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Gets additional details about the exception
    /// </summary>
    public object? Details { get; }

    /// <summary>
    /// Initializes a new instance of application exception
    /// </summary>
    /// <param name="message">The error message</param>
    /// <param name="code">The error code</param>
    /// <param name="statusCode">The HTTP status code</param>
    /// <param name="details">Additional error details</param>
    protected AppException(string message, string code, int statusCode = 400, object? details = null)
        : base(message)
    {
        Code = code;
        StatusCode = statusCode;
        Details = details;
    }
}