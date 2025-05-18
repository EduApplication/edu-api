using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing user role entities
/// </summary>
public interface IRoleRepository : IBaseRepository<Role, int>
{
    /// <summary>
    /// Retrieves a role by its name
    /// </summary>
    /// <param name="roleName">The name of the role to retrieve (e.g., "Administrator", "Teacher", "Student", "Parent")</param>
    /// <returns>The role entity with the specified name</returns>
    Task<Role> GetRoleByNameAsync(string roleName);
}