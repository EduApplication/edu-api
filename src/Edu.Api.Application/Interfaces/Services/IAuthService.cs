using Edu.Api.Application.DTOs.Auth;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Interfaces.Services;

/// <summary>
/// Service interface for user authentication and token generation
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Authenticates a user based on login credentials
    /// </summary>
    /// <param name="loginDto">DTO containing user login credentials (email and password)</param>
    /// <returns>Authentication response with user information and access token</returns>
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);

    /// <summary>
    /// Generates a JWT token for an authenticated user
    /// </summary>
    /// <param name="user">The user entity for which to generate the token</param>
    /// <returns>A JWT token string for the user</returns>
    Task<string> GenerateJwtTokenAsync(User user);
}