namespace Edu.Api.Application.DTOs.Lesson;

/// <summary>
/// Detailed representation of a lesson with class, subject, teacher and attachment information
/// </summary>
public class LessonDetailsDto
{
    /// <summary>
    /// Unique identifier of the lesson
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Identifier of the class-subject-teacher relationship for this lesson
    /// </summary>
    public Guid ClassSubjectTeacherId { get; set; }

    /// <summary>
    /// Name of the subject taught in this lesson
    /// </summary>
    public string? SubjectName { get; set; }

    /// <summary>
    /// Name of the class for which this lesson is scheduled
    /// </summary>
    public string? ClassName { get; set; }

    /// <summary>
    /// First name of the teacher conducting this lesson
    /// </summary>
    public string? TeacherFirstName { get; set; }

    /// <summary>
    /// Last name of the teacher conducting this lesson
    /// </summary>
    public string? TeacherLastName { get; set; }

    /// <summary>
    /// Email address of the teacher
    /// </summary>
    public string? TeacherEmail { get; set; }

    /// <summary>
    /// Date and time when the lesson starts
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Date and time when the lesson ends
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Room or location where the lesson is held
    /// </summary>
    public string? Room { get; set; }

    /// <summary>
    /// Main topic or title of the lesson
    /// </summary>
    public string? Topic { get; set; }

    /// <summary>
    /// Detailed description of the lesson content
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Collection of attachments associated with this lesson
    /// </summary>
    public ICollection<LessonAttachmentInfoDto>? Attachments { get; set; }
}