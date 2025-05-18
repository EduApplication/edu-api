using Edu.Api.Application.DTOs.Attachment;

namespace Edu.Api.Application.Interfaces.Services;

/// <summary>
/// Service interface for managing attachments in the educational system
/// </summary>
public interface IAttachmentService : IBaseService<CreateAttachmentDto, AttachmentDetailsDto, AttachmentDto, Guid>
{
    /// <summary>
    /// Retrieves all attachments associated with a specific class
    /// </summary>
    /// <param name="classId">The unique identifier of the class</param>
    /// <returns>A collection of attachment DTOs for the specified class</returns>
    Task<IEnumerable<AttachmentDto>> GetAttachmentsByClassIdAsync(Guid classId);

    /// <summary>
    /// Retrieves all attachments created by a specific teacher
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher</param>
    /// <returns>A collection of attachment DTOs created by the specified teacher</returns>
    Task<IEnumerable<AttachmentDto>> GetAttachmentsByTeacherIdAsync(Guid teacherId);

    /// <summary>
    /// Retrieves all attachments related to a specific subject
    /// </summary>
    /// <param name="subjectId">The unique identifier of the subject</param>
    /// <returns>A collection of attachment DTOs for the specified subject</returns>
    Task<IEnumerable<AttachmentDto>> GetAttachmentsBySubjectIdAsync(Guid subjectId);
}