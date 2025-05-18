using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Domain.Entities;
using Edu.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Edu.Api.Infrastructure.Repositories;

/// <summary>
/// Repository for managing class-subject-teacher relationship entities
/// </summary>
public class ClassSubjectTeacherRepository(AppDbContext context) : BaseRepository<ClassSubjectTeacher, Guid>(context), IClassSubjectTeacherRepository
{
    /// <summary>
    /// Retrieves classes and subjects assigned to a specific teacher
    /// </summary>
    /// <param name="teacherId">The teacher identifier</param>
    /// <returns>A collection of class-subject-teacher relationships for the specified teacher</returns>
    public async Task<IEnumerable<ClassSubjectTeacher>> GetClassesSubjectByTeacherIdAsync(Guid teacherId)
    {
        return await _context.ClassSubjectTeacher
            .Include(c => c.TeacherSubject)
            .Include(c => c.Class)
            .Where(c => c.TeacherSubject.TeacherId == teacherId)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves classes and teachers assigned to a specific subject
    /// </summary>
    /// <param name="subjectId">The subject identifier</param>
    /// <returns>A collection of class-subject-teacher relationships for the specified subject</returns>
    public async Task<IEnumerable<ClassSubjectTeacher>> GetClassesTeacherBySubjectIdAsync(Guid subjectId)
    {
        var classTeacherList = await _context.ClassSubjectTeacher
            .Include(c => c.TeacherSubject)
            .Include(c => c.Class)
            .Where(c => c.TeacherSubject.SubjectId == subjectId)
            .ToListAsync();
        return classTeacherList;
    }

    /// <summary>
    /// Retrieves teachers and subjects assigned to a specific class
    /// </summary>
    /// <param name="classId">The class identifier</param>
    /// <returns>A collection of class-subject-teacher relationships for the specified class</returns>
    public async Task<IEnumerable<ClassSubjectTeacher>> GetTeacherSubjectByClassIdAsync(Guid classId)
    {
        var teacherSubjectList = await _context.ClassSubjectTeacher
            .Include(c => c.TeacherSubject)
            .Include(c => c.Class)
            .Where(c => c.ClassId == classId)
            .ToListAsync();
        return teacherSubjectList;
    }
}