using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing user entities
/// </summary>
public interface IUserRepository : IBaseRepository<User, Guid>
{
    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address of the user to retrieve</param>
    /// <returns>The user entity with the specified email, or null if not found</returns>
    Task<User?> GetUserByEmailAsync(string? email);
}