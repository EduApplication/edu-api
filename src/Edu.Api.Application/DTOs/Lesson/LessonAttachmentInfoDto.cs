namespace Edu.Api.Application.DTOs.Lesson;

/// <summary>
/// Basic information about an attachment associated with a lesson
/// </summary>
public class LessonAttachmentInfoDto
{
    /// <summary>
    /// Unique identifier of the attachment
    /// </summary>
    public Guid AttachmentId { get; set; }

    /// <summary>
    /// Title of the attachment
    /// </summary>
    public string? Title { get; set; }
}