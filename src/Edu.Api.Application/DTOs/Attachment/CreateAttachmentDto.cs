namespace Edu.Api.Application.DTOs.Attachment;

/// <summary>
/// Data transfer object for creating a new attachment
/// </summary>
public class CreateAttachmentDto
{
    /// <summary>
    /// Title of the attachment
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Detailed description of the attachment
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Identifier of the lesson to which this attachment belongs
    /// </summary>
    public Guid LessonId { get; set; }

    /// <summary>
    /// Deadline for the attachment submission
    /// </summary>
    public DateTime DueDate { get; set; }
}