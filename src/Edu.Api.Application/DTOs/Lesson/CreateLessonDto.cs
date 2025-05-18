namespace Edu.Api.Application.DTOs.Lesson;

/// <summary>
/// Data transfer object for creating a new lesson
/// </summary>
public class CreateLessonDto
{
    /// <summary>
    /// Identifier of the class-subject-teacher relationship for this lesson
    /// </summary>
    public Guid ClassSubjectTeacherId { get; set; }

    /// <summary>
    /// Date and time when the lesson starts
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Date and time when the lesson ends
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Room or location where the lesson will be held
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
    /// Indicates whether this is a recurring lesson
    /// </summary>
    public bool IsRecurring { get; set; }

    /// <summary>
    /// Day of the week for recurring lessons
    /// </summary>
    public DayOfWeek? DayOfWeek { get; set; }
}