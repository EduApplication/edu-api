using Microsoft.EntityFrameworkCore;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Domain.Entities;
using Edu.Api.Infrastructure.Data;

namespace Edu.Api.Infrastructure.Repositories;

/// <summary>
/// Repository for managing role entities
/// </summary>
public class RoleRepository(AppDbContext dbContext) : BaseRepository<Role, int>(dbContext), IRoleRepository
{
    /// <summary>
    /// Retrieves a role by its name
    /// </summary>
    /// <param name="roleName">The name of the role</param>
    /// <returns>The role if found, otherwise a new empty role</returns>
    public async Task<Role> GetRoleByNameAsync(string roleName)
    {
        return await _dbSet.FirstOrDefaultAsync(r => r.Name == roleName) ?? new Role();
    }
}