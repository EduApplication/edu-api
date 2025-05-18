using Microsoft.AspNetCore.Mvc;
using Edu.Api.Application.DTOs.Auth;
using Edu.Api.Application.Interfaces.Services;

namespace Edu.Api.WebApi.Controllers;

/// <summary>
/// Controller for authentication operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    /// <summary>
    /// Authenticates a user and returns an access token
    /// </summary>
    /// <param name="loginDto">User login credentials</param>
    /// <response code="200">Returns the authentication response with access token and user details</response>
    /// <response code="400">If the credentials are invalid or missing required fields</response>
    /// <response code="401">If authentication fails due to incorrect credentials</response>
    /// <response code="500">If there's a server error during authentication</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);
        return Ok(result);
    }
}
