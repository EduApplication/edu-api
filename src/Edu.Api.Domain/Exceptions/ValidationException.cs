namespace Edu.Api.Domain.Exceptions;

/// <summary>
/// Exception thrown when data validation fails
/// </summary>
public class ValidationException : AppException
{
    /// <summary>
    /// Initializes a new instance of the validation exception
    /// </summary>
    /// <param name="message">The validation error message</param>
    /// <param name="errors">Dictionary of field validation errors</param>
    public ValidationException(string message, Dictionary<string, string[]> errors)
        : base(message,
               "validation_failed",
               400,
               errors)
    {
    }
}

