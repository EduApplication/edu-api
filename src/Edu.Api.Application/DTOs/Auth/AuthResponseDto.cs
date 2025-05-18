namespace Edu.Api.Application.DTOs.Auth;

/// <summary>
/// Response object returned after successful authentication
/// </summary>
public class AuthResponseDto
{
    /// <summary>
    /// JWT authentication token used for subsequent API requests
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// Unique identifier of the authenticated user
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Email address of the authenticated user
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// First name of the authenticated user
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name of the authenticated user
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Role of the authenticated user in the system (e.g., Student, Teacher, Administrator)
    /// </summary>
    public string? RoleName { get; set; }

    /// <summary>
    /// Date and time when the authentication token expires
    /// </summary>
    public DateTime TokenExpires { get; set; }
}