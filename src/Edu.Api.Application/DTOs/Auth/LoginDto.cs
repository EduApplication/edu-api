namespace Edu.Api.Application.DTOs.Auth;

/// <summary>
/// Data transfer object for user authentication
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Email address used for authentication
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// User password for authentication
    /// </summary>
    public string? Password { get; set; }
}