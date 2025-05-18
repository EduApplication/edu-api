namespace Edu.Api.Application.DTOs.User;

/// <summary>
/// Data transfer object for creating a new user
/// </summary>
public class CreateUserDto
{
    /// <summary>
    /// Gets or sets the email address of the user
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the password for the user account
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the user
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the role identifier for the user
    /// </summary>
    public int RoleId { get; set; }
}