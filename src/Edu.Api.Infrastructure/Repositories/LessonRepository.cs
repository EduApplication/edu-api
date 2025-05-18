using Microsoft.EntityFrameworkCore;
using Edu.Api.Application.DTOs.Lesson;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Domain.Entities;
using Edu.Api.Infrastructure.Data;

namespace Edu.Api.Infrastructure.Repositories;

/// <summary>
/// Repository for managing lesson entities
/// </summary>
public class LessonRepository(AppDbContext context) : BaseRepository<Lesson, Guid>(context), ILessonRepository
{
    /// <summary>
    /// Retrieves a lesson by its identifier with all related entities
    /// </summary>
    /// <param name="id">The lesson identifier</param>
    /// <returns>The lesson if found, otherwise null</returns>
    public override async Task<Lesson?> GetByIdAsync(Guid id)
    {
        return await _context.Lessons
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.Class)
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.TeacherSubject)
                    .ThenInclude(ts => ts.Teacher)
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.TeacherSubject)
                    .ThenInclude(ts => ts.Subject)
            .Include(l => l.Attachments)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    /// <summary>
    /// Retrieves all lessons with their related entities
    /// </summary>
    /// <returns>A collection of all lessons</returns>
    public override async Task<IEnumerable<Lesson>> GetAllAsync()
    {
        return await _context.Lessons
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.Class)
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.TeacherSubject)
                    .ThenInclude(ts => ts.Teacher)
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.TeacherSubject)
                    .ThenInclude(ts => ts.Subject)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves lessons for a specific class
    /// </summary>
    /// <param name="classId">The class identifier</param>
    /// <returns>A collection of lessons for the specified class</returns>
    public async Task<IEnumerable<Lesson>> GetLessonsByClassIdAsync(Guid classId)
    {
        return await _context.Lessons
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.Class)
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.TeacherSubject)
                    .ThenInclude(ts => ts.Subject)
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.TeacherSubject)
                    .ThenInclude(ts => ts.Teacher)
            .Where(l => l.ClassSubjectTeacher.ClassId == classId)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves lessons for a specific teacher
    /// </summary>
    /// <param name="teacherId">The teacher identifier</param>
    /// <returns>A collection of lessons for the specified teacher</returns>
    public async Task<IEnumerable<Lesson>> GetLessonsByTeacherIdAsync(Guid teacherId)
    {
        return await _context.Lessons
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.Class)
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.TeacherSubject)
                    .ThenInclude(ts => ts.Subject)
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.TeacherSubject)
                    .ThenInclude(ts => ts.Teacher)
            .Where(l => l.ClassSubjectTeacher.TeacherSubject.TeacherId == teacherId)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves lessons for a specific subject
    /// </summary>
    /// <param name="subjectId">The subject identifier</param>
    /// <returns>A collection of lessons for the specified subject</returns>
    public async Task<IEnumerable<Lesson>> GetLessonsBySubjectIdAsync(Guid subjectId)
    {
        return await _context.Lessons
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.Class)
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.TeacherSubject)
                    .ThenInclude(ts => ts.Subject)
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.TeacherSubject)
                    .ThenInclude(ts => ts.Teacher)
            .Where(l => l.ClassSubjectTeacher.TeacherSubject.SubjectId == subjectId)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves lessons based on specified filter criteria
    /// </summary>
    /// <param name="filter">The schedule filter criteria</param>
    /// <returns>A collection of lessons matching the filter criteria, ordered by start time</returns>
    public async Task<IEnumerable<Lesson>> GetLessonsByFilterAsync(ScheduleFilterDto filter)
    {
        IQueryable<Lesson> query = _context.Lessons
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.Class)
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.TeacherSubject)
                    .ThenInclude(ts => ts.Subject)
            .Include(l => l.ClassSubjectTeacher)
                .ThenInclude(cst => cst.TeacherSubject)
                    .ThenInclude(ts => ts.Teacher);

        if (filter.StartDate.HasValue)
            query = query.Where(l => l.StartTime >= filter.StartDate.Value);

        if (filter.EndDate.HasValue)
            query = query.Where(l => l.EndTime <= filter.EndDate.Value);

        if (filter.ClassId.HasValue)
            query = query.Where(l => l.ClassSubjectTeacher.ClassId == filter.ClassId.Value);

        if (filter.TeacherId.HasValue)
            query = query.Where(l => l.ClassSubjectTeacher.TeacherSubject.TeacherId == filter.TeacherId.Value);

        if (filter.SubjectId.HasValue)
            query = query.Where(l => l.ClassSubjectTeacher.TeacherSubject.SubjectId == filter.SubjectId.Value);

        return await query.OrderBy(l => l.StartTime).ToListAsync();
    }
}