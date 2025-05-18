using System.Security.Claims;
using Edu.Api.Infrastructure.Identity;

namespace Edu.Api.WebApi.Middleware;

/// <summary>
/// Middleware for populating the current user context from authentication claims
/// </summary>
public class CurrentUserMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    /// <summary>
    /// Processes an HTTP request to extract user information from claims
    /// </summary>
    /// <param name="context">The HTTP context for the request</param>
    /// <param name="userContext">The user context to populate</param>
    public async Task InvokeAsync(HttpContext context, IUserContext userContext)
    {
        var user = context.User;
        if (user.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var emailClaim = user.FindFirst(ClaimTypes.Email)?.Value;
            var firstNameClaim = user.FindFirst(ClaimTypes.GivenName)?.Value;
            var lastNameClaim = user.FindFirst(ClaimTypes.Surname)?.Value;
            var roleClaim = user.FindFirst(ClaimTypes.Role)?.Value;
            if (Guid.TryParse(userIdClaim, out var userId))
            {
                userContext.UserId = userId;
                userContext.Email = emailClaim;
                userContext.FirstName = firstNameClaim;
                userContext.LastName = lastNameClaim;
                userContext.Role = roleClaim;
            }
        }
        await _next(context);
    }
}