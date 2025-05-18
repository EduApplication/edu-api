using Microsoft.EntityFrameworkCore;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Domain.Entities;
using Edu.Api.Infrastructure.Data;

namespace Edu.Api.Infrastructure.Repositories;

/// <summary>
/// Repository for managing teacher-subject relationship entities
/// </summary>
public class TeacherSubjectRepository(AppDbContext context) : BaseRepository<TeacherSubject, Guid>(context), ITeacherSubjectRepository
{
    /// <summary>
    /// Retrieves teachers assigned to a specific subject
    /// </summary>
    /// <param name="subjectId">The subject identifier</param>
    /// <returns>A collection of teacher user entities with their roles</returns>
    public async Task<IEnumerable<User>> GetTeachersBySubjectIdAsync(Guid subjectId)
    {
        return await _context.TeacherSubjects
            .Where(ts => ts.SubjectId == subjectId)
            .Include(ts => ts.Teacher)
                .ThenInclude(t => t.Role)
            .Select(ts => ts.Teacher)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves subjects assigned to a specific teacher
    /// </summary>
    /// <param name="teacherId">The teacher identifier</param>
    /// <returns>A collection of subject entities</returns>
    public async Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(Guid teacherId)
    {
        return await _context.TeacherSubjects
            .Where(ts => ts.TeacherId == teacherId)
            .Include(ts => ts.Subject)
            .Select(ts => ts.Subject)
            .ToListAsync();
    }
}