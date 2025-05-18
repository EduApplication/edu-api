using Microsoft.EntityFrameworkCore;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Domain.Entities;
using Edu.Api.Infrastructure.Data;

namespace Edu.Api.Infrastructure.Repositories;

/// <summary>
/// Repository for managing grade entities
/// </summary>
public class GradeRepository(AppDbContext context) : BaseRepository<Grade, Guid>(context), IGradeRepository
{
    /// <summary>
    /// Retrieves a grade by its identifier with all related entities
    /// </summary>
    /// <param name="id">The grade identifier</param>
    /// <returns>The grade if found, otherwise null</returns>
    public override async Task<Grade?> GetByIdAsync(Guid id)
    {
        return await _context.Grades
            .Include(g => g.Student)
            .Include(g => g.Subject)
            .Include(g => g.Teacher)
            .Include(g => g.GradeType)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    /// <summary>
    /// Retrieves all grades with their related entities
    /// </summary>
    /// <returns>A collection of all grades</returns>
    public override async Task<IEnumerable<Grade>> GetAllAsync()
    {
        return await _context.Grades
            .Include(g => g.Student)
            .Include(g => g.Subject)
            .Include(g => g.Teacher)
            .Include(g => g.GradeType)
        .ToListAsync();
    }

    /// <summary>
    /// Retrieves grades for a specific student
    /// </summary>
    /// <param name="studentId">The student identifier</param>
    /// <returns>A collection of grades for the specified student</returns>
    public async Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(Guid studentId)
    {
        return await _context.Grades
            .Include(g => g.Student)
            .Include(g => g.Subject)
            .Include(g => g.Teacher)
            .Include(g => g.GradeType)
            .Where(g => g.StudentId == studentId)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves grades for a specific student in a specific subject
    /// </summary>
    /// <param name="studentId">The student identifier</param>
    /// <param name="subjectId">The subject identifier</param>
    /// <returns>A collection of grades for the specified student in the specified subject</returns>
    public async Task<IEnumerable<Grade>> GetGradesBySubjectForStudentAsync(Guid studentId, Guid subjectId)
    {
        return await _context.Grades
            .Include(g => g.Student)
            .Include(g => g.Subject)
            .Include(g => g.Teacher)
            .Include(g => g.GradeType)
            .Where(g => g.StudentId == studentId && g.SubjectId == subjectId)
            .ToListAsync();
    }
}