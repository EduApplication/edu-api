namespace Edu.Api.Application.DTOs.Attachment;

/// <summary>
/// Basic representation of an attachment for list views
/// </summary>
public class AttachmentDto
{
    /// <summary>
    /// Unique identifier of the attachment
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Title of the attachment
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Detailed description of the attachment
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Deadline for the attachment submission
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Date when the attachment was assigned
    /// </summary>
    public DateTime AssignedDate { get; set; }
}