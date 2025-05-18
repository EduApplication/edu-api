namespace Edu.Api.Application.DTOs.Auth;

/// <summary>
/// Data transfer object for user registration
/// </summary>
public class RegisterDto
{
    /// <summary>
    /// Email address of the new user, used as login identifier
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Password for the new user account
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// First name of the new user
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name of the new user
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Contact phone number of the new user
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Role identifier that determines user permissions in the system
    /// </summary>
    public int RoleId { get; set; }
}