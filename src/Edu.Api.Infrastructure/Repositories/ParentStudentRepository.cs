using Microsoft.EntityFrameworkCore;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Domain.Entities;
using Edu.Api.Infrastructure.Data;

namespace Edu.Api.Infrastructure.Repositories;

/// <summary>
/// Repository for managing parent-student relationship entities
/// </summary>
public class ParentStudentRepository(AppDbContext context) : BaseRepository<ParentStudent, Guid>(context), IParentStudentRepository
{
    /// <summary>
    /// Retrieves the identifiers of students associated with a specific parent
    /// </summary>
    /// <param name="parentId">The parent identifier</param>
    /// <returns>A collection of student identifiers</returns>
    public async Task<IEnumerable<Guid>> GetStudentIdsByParentIdAsync(Guid parentId)
    {
        return await _context.ParentStudents
            .Where(ps => ps.ParentId == parentId)
            .Select(ps => ps.StudentId)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves student users associated with a specific parent
    /// </summary>
    /// <param name="parentId">The parent identifier</param>
    /// <returns>A collection of student user entities with their roles</returns>
    public async Task<IEnumerable<User>> GetStudentsByParentIdAsync(Guid parentId)
    {
        return await _context.Users
            .Include(u => u.Role)
            .Where(u => u.StudentRelationships.Any(ps => ps.ParentId == parentId))
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves parent users associated with a specific student
    /// </summary>
    /// <param name="studentId">The student identifier</param>
    /// <returns>A collection of parent user entities with their roles</returns>
    public async Task<IEnumerable<User>> GetParentsByStudentIdAsync(Guid studentId)
    {
        return await _context.Users
            .Include(u => u.Role)
            .Where(u => u.ParentRelationships.Any(ps => ps.StudentId == studentId))
            .ToListAsync();
    }
}