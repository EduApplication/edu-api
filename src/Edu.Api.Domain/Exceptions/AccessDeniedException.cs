namespace Edu.Api.Domain.Exceptions;

/// <summary>
/// Exception thrown when access to a resource is unauthorized
/// </summary>
public class AccessDeniedException : AppException
{
    /// <summary>
    /// Initializes a new instance of the access denied exception
    /// </summary>
    /// <param name="resource">The resource being accessed</param>
    /// <param name="operation">The operation being attempted</param>
    public AccessDeniedException(string resource, string operation)
        : base($"Access denied to {operation} on {resource}",
               "access_denied",
               403)
    {
    }
}