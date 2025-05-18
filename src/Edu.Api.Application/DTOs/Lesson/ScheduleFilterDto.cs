namespace Edu.Api.Application.DTOs.Lesson;

/// <summary>
/// Filter criteria for retrieving lesson schedules
/// </summary>
public class ScheduleFilterDto
{
    /// <summary>
    /// Optional start date to filter lessons
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Optional end date to filter lessons
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Optional class identifier to filter lessons for a specific class
    /// </summary>
    public Guid? ClassId { get; set; }

    /// <summary>
    /// Optional teacher identifier to filter lessons by a specific teacher
    /// </summary>
    public Guid? TeacherId { get; set; }

    /// <summary>
    /// Optional subject identifier to filter lessons for a specific subject
    /// </summary>
    public Guid? SubjectId { get; set; }
}