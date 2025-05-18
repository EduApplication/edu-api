using Microsoft.EntityFrameworkCore;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Domain.Entities;
using Edu.Api.Infrastructure.Data;

namespace Edu.Api.Infrastructure.Repositories;

/// <summary>
/// Repository for managing subject entities
/// </summary>
public class SubjectRepository(AppDbContext context) : BaseRepository<Subject, Guid>(context), ISubjectRepository
{
    /// <summary>
    /// Retrieves a subject by its identifier
    /// </summary>
    /// <param name="id">The subject identifier</param>
    /// <returns>The subject if found, otherwise null</returns>
    public override async Task<Subject?> GetByIdAsync(Guid id)
    {
        return await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    /// <summary>
    /// Retrieves all subjects
    /// </summary>
    /// <returns>A collection of all subjects</returns>
    public override async Task<IEnumerable<Subject>> GetAllAsync()
    {
        return await _context.Subjects
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a subject by its identifier with related teacher information
    /// </summary>
    /// <param name="id">The subject identifier</param>
    /// <returns>The subject with teacher relationships if found, otherwise null</returns>
    public async Task<Subject?> GetSubjectWithTeachersAsync(Guid id)
    {
        return await _context.Subjects
            .Include(s => s.TeacherSubjects)
                .ThenInclude(ts => ts.Teacher)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    /// <summary>
    /// Retrieves all subjects with their related teacher information
    /// </summary>
    /// <returns>A collection of all subjects with teacher relationships</returns>
    public async Task<IEnumerable<Subject>> GetAllSubjectsWithTeachersAsync()
    {
        return await _context.Subjects
            .Include(s => s.TeacherSubjects)
                .ThenInclude(ts => ts.Teacher)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves subjects taught by a specific teacher
    /// </summary>
    /// <param name="teacherId">The teacher identifier</param>
    /// <returns>A collection of subjects taught by the specified teacher</returns>
    public async Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(Guid teacherId)
    {
        return await _context.TeacherSubjects
            .Where(ts => ts.TeacherId == teacherId)
            .Include(ts => ts.Subject)
            .Select(ts => ts.Subject)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves subjects that a specific student is enrolled in
    /// </summary>
    /// <param name="studentId">The student identifier</param>
    /// <returns>A collection of subjects the student is studying</returns>
    public async Task<IEnumerable<Subject>> GetSubjectsByStudentIdAsync(Guid studentId)
    {
        var studentClasses = await _context.StudentClasses
            .Where(sc => sc.StudentId == studentId)
            .Select(sc => sc.ClassId)
            .ToListAsync();
        if (studentClasses.Count == 0)
            return [];
        var lessons = await _context.Lessons
            .Where(l => studentClasses.Contains(l.ClassSubjectTeacher.ClassId))
            .Select(l => l.ClassSubjectTeacher.TeacherSubject.SubjectId)
            .Distinct()
            .ToListAsync();
        return await _context.Subjects
            .Where(s => lessons.Contains(s.Id))
            .ToListAsync();
    }

    /// <summary>
    /// Checks if a teacher teaches a specific subject
    /// </summary>
    /// <param name="teacherId">The teacher identifier</param>
    /// <param name="subjectId">The subject identifier</param>
    /// <returns>True if the teacher teaches the subject, otherwise false</returns>
    public async Task<bool> IsTeacherOfSubjectAsync(Guid teacherId, Guid subjectId)
    {
        return await _context.TeacherSubjects
            .AnyAsync(ts => ts.TeacherId == teacherId && ts.SubjectId == subjectId);
    }

    /// <summary>
    /// Assigns a teacher to a subject if not already assigned
    /// </summary>
    /// <param name="subjectId">The subject identifier</param>
    /// <param name="teacherId">The teacher identifier</param>
    /// <returns>The identifier of the added entity</returns>
    public async Task<Guid> AddTeacherToSubjectAsync(Guid subjectId, Guid teacherId)
    {
        var entity = new TeacherSubject
        {
            SubjectId = subjectId,
            TeacherId = teacherId
        };

        _context.TeacherSubjects.Add(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    /// <summary>
    /// Removes a teacher from a subject
    /// </summary>
    /// <param name="subjectId">The subject identifier</param>
    /// <param name="teacherId">The teacher identifier</param>
    public async Task RemoveTeacherFromSubjectAsync(Guid subjectId, Guid teacherId)
    {
        var teacherSubject = await _context.TeacherSubjects
            .FirstOrDefaultAsync(ts => ts.SubjectId == subjectId && ts.TeacherId == teacherId);
        if (teacherSubject != null)
        {
            _context.TeacherSubjects.Remove(teacherSubject);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Retrieves student-class relationships for a list of class identifiers
    /// </summary>
    /// <param name="classIds">A list of class identifiers</param>
    /// <returns>A collection of student-class relationships for the specified classes</returns>
    public async Task<IEnumerable<StudentClass>> GetStudentClass(List<Guid> classIds)
    {
        return await _context.StudentClasses
            .Where(sc => classIds.Contains(sc.ClassId))
            .ToListAsync();
    }
}