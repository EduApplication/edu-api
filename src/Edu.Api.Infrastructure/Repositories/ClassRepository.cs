using Microsoft.EntityFrameworkCore;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Domain.Entities;
using Edu.Api.Infrastructure.Data;

namespace Edu.Api.Infrastructure.Repositories;

/// <summary>
/// Repository for managing class entities
/// </summary>
public class ClassRepository(AppDbContext context) : BaseRepository<Class, Guid>(context), IClassRepository
{
    /// <summary>
    /// Retrieves a class by its identifier with related entities
    /// </summary>
    /// <param name="id">The class identifier</param>
    /// <returns>The class if found, otherwise null</returns>
    public override async Task<Class?> GetByIdAsync(Guid id)
    {
        return await _context.Classes
            .Include(c => c.ClassTeacher)
            .Include(c => c.StudentClasses)
                .ThenInclude(sc => sc.Student)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    /// <summary>
    /// Retrieves all classes with their class teachers
    /// </summary>
    /// <returns>A collection of all classes</returns>
    public override async Task<IEnumerable<Class>> GetAllAsync()
    {
        return await _context.Classes
            .Include(c => c.ClassTeacher)
        .ToListAsync();
    }

    /// <summary>
    /// Retrieves classes associated with a specific teacher
    /// </summary>
    /// <param name="teacherId">The teacher identifier</param>
    /// <returns>A collection of classes where the teacher is either the class teacher or teaches a subject</returns>
    public async Task<IEnumerable<Class>> GetClassesByTeacherIdAsync(Guid teacherId)
    {
        var classes = await _context.Classes
            .Include(c => c.ClassTeacher)
            .Where(c => c.ClassTeacherId == teacherId)
            .ToListAsync();
        var teacherSubjects = await _context.TeacherSubjects
            .Where(ts => ts.TeacherId == teacherId)
            .Select(ts => ts.SubjectId)
            .ToListAsync();
        var additionalClasses = await _context.Lessons
            .Where(l => teacherSubjects.Contains(l.ClassSubjectTeacher.TeacherSubject.SubjectId) && l.ClassSubjectTeacher.TeacherSubject.TeacherId == teacherId)
            .Select(l => l.ClassSubjectTeacher.ClassId)
            .Distinct()
            .ToListAsync();
        if (additionalClasses.Count != 0)
        {
            var teachingClasses = await _context.Classes
                .Include(c => c.ClassTeacher)
                .Where(c => additionalClasses.Contains(c.Id) && !classes.Select(cls => cls.Id).Contains(c.Id))
                .ToListAsync();
            classes.AddRange(teachingClasses);
        }
        return classes;
    }

    /// <summary>
    /// Checks if a teacher is associated with a specific class
    /// </summary>
    /// <param name="teacherId">The teacher identifier</param>
    /// <param name="classId">The class identifier</param>
    /// <returns>True if the teacher is the class teacher or teaches a subject in the class, otherwise false</returns>
    public async Task<bool> IsTeacherOfClassAsync(Guid teacherId, Guid classId)
    {
        var isClassTeacher = await _context.Classes
            .AnyAsync(c => c.Id == classId && c.ClassTeacherId == teacherId);
        if (isClassTeacher)
            return true;
        var teachesInClass = await _context.Lessons
            .AnyAsync(l => l.ClassSubjectTeacher.ClassId == classId && l.ClassSubjectTeacher.TeacherSubject.TeacherId == teacherId);
        return teachesInClass;
    }
}