using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing attachment entities
/// </summary>
public interface IAttachmentRepository : IBaseRepository<Attachment, Guid>
{
    /// <summary>
    /// Retrieves all attachments associated with a specific class
    /// </summary>
    /// <param name="classId">The unique identifier of the class</param>
    /// <returns>A collection of attachments for the specified class</returns>
    Task<IEnumerable<Attachment>> GetAttachmentsByClassIdAsync(Guid classId);

    /// <summary>
    /// Retrieves all attachments created by a specific teacher
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher</param>
    /// <returns>A collection of attachments created by the specified teacher</returns>
    Task<IEnumerable<Attachment>> GetAttachmentsByTeacherIdAsync(Guid teacherId);

    /// <summary>
    /// Retrieves all attachments related to a specific subject
    /// </summary>
    /// <param name="subjectId">The unique identifier of the subject</param>
    /// <returns>A collection of attachments for the specified subject</returns>
    Task<IEnumerable<Attachment>> GetAttachmentsBySubjectIdAsync(Guid subjectId);
}