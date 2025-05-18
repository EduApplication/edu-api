using Microsoft.EntityFrameworkCore;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Domain.Entities;
using Edu.Api.Infrastructure.Data;

namespace Edu.Api.Infrastructure.Repositories;

/// <summary>
/// Repository for managing user entities
/// </summary>
public class UserRepository(AppDbContext context) : BaseRepository<User, Guid>(context), IUserRepository
{
    /// <summary>
    /// Retrieves a user by its identifier with all related entities
    /// </summary>
    /// <param name="id">The user identifier</param>
    /// <returns>The user with all relationships if found, otherwise null</returns>
    public override async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.Role)
            .Include(u => u.TeacherSubjects)
                .ThenInclude(ts => ts.Subject)
            .Include(u => u.StudentClasses)
                .ThenInclude(sc => sc.Class)
            .Include(u => u.ParentRelationships)
               .ThenInclude(pr => pr.Student)
            .Include(u => u.StudentRelationships)
                .ThenInclude(sr => sr.Parent)
            .Include(u => u.ClassesAsTutor)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    /// <summary>
    /// Retrieves all users with their roles
    /// </summary>
    /// <returns>A collection of all users with their roles</returns>
    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Role)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <returns>The user with their role if found, otherwise null</returns>
    public async Task<User?> GetUserByEmailAsync(string? email)
    {
        return await _dbSet
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}