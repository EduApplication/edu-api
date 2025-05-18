using Edu.Api.Application.DTOs.Document;

namespace Edu.Api.Application.DTOs.Attachment;

/// <summary>
/// Detailed representation of an attachment with related information
/// </summary>
public class AttachmentDetailsDto
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

    /// <summary>
    /// Name of the subject associated with this attachment
    /// </summary>
    public string? SubjectName { get; set; }

    /// <summary>
    /// Name of the class associated with this attachment
    /// </summary>
    public string? ClassName { get; set; }

    /// <summary>
    /// First name of the teacher who assigned the attachment
    /// </summary>
    public string? TeacherFirstName { get; set; }

    /// <summary>
    /// Last name of the teacher who assigned the attachment
    /// </summary>
    public string? TeacherLastName { get; set; }

    /// <summary>
    /// Collection of documents associated with this attachment
    /// </summary>
    public ICollection<DocumentDto>? Documents { get; set; }
}