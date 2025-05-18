using Edu.Api.Domain.Common;

namespace Edu.Api.Infrastructure.Identity;

/// <summary>
/// Infrastructure-specific interface for accessing and modifying the current user context
/// </summary>
public interface IUserContext : ICurrentUser
{
    /// <summary>
    /// Gets or sets the unique identifier of the current user
    /// </summary>
    new Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the email address of the current user
    /// </summary>
    new string? Email { get; set; }

    /// <summary>
    /// Gets or sets the first name of the current user
    /// </summary>
    new string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the current user
    /// </summary>
    new string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the role name of the current user
    /// </summary>
    new string? Role { get; set; }
}