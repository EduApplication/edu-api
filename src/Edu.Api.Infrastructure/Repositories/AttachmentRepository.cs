using Microsoft.EntityFrameworkCore;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Domain.Entities;
using Edu.Api.Infrastructure.Data;

namespace Edu.Api.Infrastructure.Repositories;

/// <summary>
/// Repository for managing attachment entities
/// </summary>
public class AttachmentRepository(AppDbContext context) : BaseRepository<Attachment, Guid>(context), IAttachmentRepository
{
    /// <summary>
    /// Retrieves an attachment by its identifier with all related entities
    /// </summary>
    /// <param name="id">The attachment identifier</param>
    /// <returns>The attachment if found, otherwise null</returns>
    public override async Task<Attachment?> GetByIdAsync(Guid id)
    {
        return await _context.Attachments
            .Include(a => a.Lesson)
                .ThenInclude(l => l.ClassSubjectTeacher)
                    .ThenInclude(cst => cst.Class)
            .Include(a => a.Lesson)
                .ThenInclude(l => l.ClassSubjectTeacher)
                    .ThenInclude(cst => cst.TeacherSubject)
                        .ThenInclude(ts => ts.Subject)
            .Include(a => a.Lesson)
                .ThenInclude(l => l.ClassSubjectTeacher)
                    .ThenInclude(cst => cst.TeacherSubject)
                        .ThenInclude(ts => ts.Teacher)
            .Include(a => a.Documents)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    /// <summary>
    /// Retrieves all attachments
    /// </summary>
    /// <returns>A collection of all attachments</returns>
    public override async Task<IEnumerable<Attachment>> GetAllAsync()
    {
        return await _context.Attachments
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves attachments for a specific class
    /// </summary>
    /// <param name="classId">The class identifier</param>
    /// <returns>A collection of attachments for the specified class</returns>
    public async Task<IEnumerable<Attachment>> GetAttachmentsByClassIdAsync(Guid classId)
    {
        return await _context.Attachments
            .Include(a => a.Lesson)
                .ThenInclude(l => l.ClassSubjectTeacher)
                    .ThenInclude(cst => cst.Class)
            .Include(a => a.Lesson)
                .ThenInclude(l => l.ClassSubjectTeacher)
                    .ThenInclude(cst => cst.TeacherSubject)
                        .ThenInclude(ts => ts.Subject)
            .Include(a => a.Lesson)
                .ThenInclude(l => l.ClassSubjectTeacher)
                    .ThenInclude(cst => cst.TeacherSubject)
                        .ThenInclude(ts => ts.Teacher)
            .Where(a => a.Lesson.ClassSubjectTeacher.ClassId == classId)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves attachments for a specific teacher
    /// </summary>
    /// <param name="teacherId">The teacher identifier</param>
    /// <returns>A collection of attachments for the specified teacher</returns>
    public async Task<IEnumerable<Attachment>> GetAttachmentsByTeacherIdAsync(Guid teacherId)
    {
        return await _context.Attachments
            .Include(a => a.Lesson)
                .ThenInclude(l => l.ClassSubjectTeacher)
                    .ThenInclude(cst => cst.Class)
            .Include(a => a.Lesson)
                .ThenInclude(l => l.ClassSubjectTeacher)
                    .ThenInclude(cst => cst.TeacherSubject)
                        .ThenInclude(ts => ts.Subject)
            .Include(a => a.Lesson)
                .ThenInclude(l => l.ClassSubjectTeacher)
                    .ThenInclude(cst => cst.TeacherSubject)
                        .ThenInclude(ts => ts.Teacher)
            .Where(a => a.Lesson.ClassSubjectTeacher.TeacherSubject.TeacherId == teacherId)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves attachments for a specific subject
    /// </summary>
    /// <param name="subjectId">The subject identifier</param>
    /// <returns>A collection of attachments for the specified subject</returns>
    public async Task<IEnumerable<Attachment>> GetAttachmentsBySubjectIdAsync(Guid subjectId)
    {
        return await _context.Attachments
            .Include(a => a.Lesson)
                .ThenInclude(l => l.ClassSubjectTeacher)
                    .ThenInclude(cst => cst.Class)
            .Include(a => a.Lesson)
                .ThenInclude(l => l.ClassSubjectTeacher)
                    .ThenInclude(cst => cst.TeacherSubject)
                        .ThenInclude(ts => ts.Subject)
            .Include(a => a.Lesson)
                .ThenInclude(l => l.ClassSubjectTeacher)
                    .ThenInclude(cst => cst.TeacherSubject)
                        .ThenInclude(ts => ts.Teacher)
            .Where(a => a.Lesson.ClassSubjectTeacher.TeacherSubject.SubjectId == subjectId)
            .ToListAsync();
    }
}