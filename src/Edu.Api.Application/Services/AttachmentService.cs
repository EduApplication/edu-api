using Edu.Api.Domain.Entities;
using Edu.Api.Application.DTOs.Attachment;
using Edu.Api.Application.Mappings;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Application.Interfaces.Services;

namespace Edu.Api.Application.Services;

/// <summary>
/// Service for managing attachments
/// </summary>
public class AttachmentService(
    IAttachmentRepository attachmentRepository,
    IMapper<Attachment, AttachmentDto, AttachmentDetailsDto, CreateAttachmentDto, Guid> attachmentMapper) :
    BaseService<Attachment, CreateAttachmentDto, AttachmentDetailsDto, AttachmentDto, Guid>(attachmentRepository, attachmentMapper), IAttachmentService
{
    private readonly IAttachmentRepository _attachmentRepository = attachmentRepository;

    /// <summary>
    /// Retrieves attachments by class identifier
    /// </summary>
    /// <param name="classId">The class identifier</param>
    /// <returns>A collection of <see cref="AttachmentDto"/> objects</returns>
    public async Task<IEnumerable<AttachmentDto>> GetAttachmentsByClassIdAsync(Guid classId)
    {
        var attachments = await _attachmentRepository.GetAttachmentsByClassIdAsync(classId);
        return attachments.Select(_mapper.MapToDto);
    }

    /// <summary>
    /// Retrieves attachments by teacher identifier
    /// </summary>
    /// <param name="teacherId">The teacher identifier</param>
    /// <returns>A collection of <see cref="AttachmentDto"/> objects</returns>
    public async Task<IEnumerable<AttachmentDto>> GetAttachmentsByTeacherIdAsync(Guid teacherId)
    {
        var attachments = await _attachmentRepository.GetAttachmentsByTeacherIdAsync(teacherId);
        return attachments.Select(_mapper.MapToDto);
    }

    /// <summary>
    /// Retrieves attachments by subject identifier
    /// </summary>
    /// <param name="subjectId">The subject identifier</param>
    /// <returns>A collection of <see cref="AttachmentDto"/> objects</returns>
    public async Task<IEnumerable<AttachmentDto>> GetAttachmentsBySubjectIdAsync(Guid subjectId)
    {
        var attachments = await _attachmentRepository.GetAttachmentsBySubjectIdAsync(subjectId);
        return attachments.Select(_mapper.MapToDto);
    }
}