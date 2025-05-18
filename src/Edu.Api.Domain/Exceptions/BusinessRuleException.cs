namespace Edu.Api.Domain.Exceptions;

/// <summary>
/// Exception thrown when a business rule is violated
/// </summary>
public class BusinessRuleException : AppException
{
    /// <summary>
    /// Initializes a new instance of the business rule exception
    /// </summary>
    /// <param name="message">The error message</param>
    /// <param name="code">Optional specific error code</param>
    public BusinessRuleException(string message, string code = "business_rule_violation")
        : base(message,
               code,
               400)
    {
    }
}