using Microsoft.EntityFrameworkCore;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Domain.Entities;
using Edu.Api.Infrastructure.Data;

namespace Edu.Api.Infrastructure.Repositories;

/// <summary>
/// Repository for managing student-class relationship entities
/// </summary>
public class StudentClassRepository(AppDbContext context) : BaseRepository<StudentClass, Guid>(context), IStudentClassRepository
{
    /// <summary>
    /// Retrieves classes that a specific student is enrolled in
    /// </summary>
    /// <param name="studentId">The student identifier</param>
    /// <returns>A collection of class entities with their class teachers and student relationships</returns>
    public async Task<IEnumerable<Class>> GetClassesByStudentIdAsync(Guid studentId)
    {
        return await _context.Classes
            .Include(c => c.ClassTeacher)
            .Include(c => c.StudentClasses)
            .Where(c => c.StudentClasses.Any(sc => sc.StudentId == studentId))
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves students enrolled in a specific class
    /// </summary>
    /// <param name="classId">The class identifier</param>
    /// <returns>A collection of student user entities with their roles and class relationships</returns>
    public async Task<IEnumerable<User>> GetStudentsByClassIdAsync(Guid classId)
    {
        return await _context.Users
            .Include(u => u.Role)
            .Include(u => u.StudentClasses)
            .Where(u => u.StudentClasses.Any(sc => sc.ClassId == classId))
            .ToListAsync();
    }
}