namespace Edu.Api.Domain.Common;

/// <summary>
/// Interface for accessing information about the currently authenticated user
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// Gets the unique identifier of the current user
    /// </summary>
    Guid UserId { get; }

    /// <summary>
    /// Gets the email address of the current user
    /// </summary>
    string? Email { get; }

    /// <summary>
    /// Gets the first name of the current user
    /// </summary>
    string? FirstName { get; }

    /// <summary>
    /// Gets the last name of the current user
    /// </summary>
    string? LastName { get; }

    /// <summary>
    /// Gets the role name of the current user
    /// </summary>
    string? Role { get; }

    /// <summary>
    /// Determines whether the current user belongs to the specified role
    /// </summary>
    /// <param name="role">The role name to check</param>
    /// <returns>True if the user is in the specified role, otherwise false</returns>
    bool IsInRole(string role);
}