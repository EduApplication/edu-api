namespace Edu.Api.Infrastructure.Identity;

/// <summary>
/// Implementation of the user context that stores current user information
/// </summary>
public class UserContext : IUserContext
{
    /// <summary>
    /// Gets or sets the unique identifier of the current user
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the email address of the current user
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the first name of the current user
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the current user
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the role name of the current user
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// Determines whether the current user belongs to the specified role
    /// </summary>
    /// <param name="role">The role name to check</param>
    /// <returns>True if the user is in the specified role, otherwise false</returns>
    public bool IsInRole(string role)
    {
        return Role == role;
    }
}