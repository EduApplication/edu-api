using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Edu.Api.Application.DTOs.Auth;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Application.Interfaces.Services;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Services;

/// <summary>
/// Service for user authentication and token generation
/// </summary>
public class AuthService(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IConfiguration configuration) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IConfiguration _configuration = configuration;

    /// <summary>
    /// Authenticates a user with email and password
    /// </summary>
    /// <param name="loginDto">Login credentials</param>
    /// <returns>Authentication response with user details and JWT token</returns>
    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginDto.Email) ?? throw new ArgumentException("Invalid email or password.");
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
        if (!isPasswordValid)
            throw new ArgumentException("Invalid email or password.");
        if (!user.IsActive)
            throw new ArgumentException("This account is deactivated.");
        user.LastLogin = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);
        var role = await _roleRepository.GetByIdAsync(user.RoleId);
        string token = await GenerateJwtTokenAsync(user);
        return new AuthResponseDto
        {
            UserId = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            RoleName = role?.Name,
            Token = token,
            TokenExpires = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:DurationInMinutes"] ?? "60"))
        };
    }

    /// <summary>
    /// Generates a JWT token for the authenticated user
    /// </summary>
    /// <param name="user">The user for whom to generate the token</param>
    /// <returns>JWT token string</returns>
    public async Task<string> GenerateJwtTokenAsync(User user)
    {
        var role = await _roleRepository.GetByIdAsync(user.RoleId);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email ?? ""),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.Role, role?.Name ?? "Unknown")
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["Jwt:Key"] ?? ""));
        var credentials = new SigningCredentials(
            key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(
            Convert.ToDouble(_configuration["Jwt:DurationInMinutes"] ?? "60"));
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}