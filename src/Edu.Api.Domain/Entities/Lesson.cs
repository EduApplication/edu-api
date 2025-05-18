using Edu.Api.Domain.Interfaces;

namespace Edu.Api.Domain.Entities;

/// <summary>
/// Represents a lesson or class session in the educational system
/// </summary>
/// <remarks>
/// A lesson is a specific time period when a teacher teaches a subject to a class
/// </remarks>
public class Lesson : IEntity<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier for the lesson
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the identifier of the class-subject-teacher relationship for this lesson
    /// </summary>
    /// <remarks>
    /// This connects the lesson to a specific class, subject, and teacher combination
    /// </remarks>
    public Guid ClassSubjectTeacherId { get; set; }

    /// <summary>
    /// Gets or sets the start time of the lesson
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets the end time of the lesson
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Gets or sets the room or location where the lesson takes place
    /// </summary>
    public string? Room { get; set; }

    /// <summary>
    /// Gets or sets the topic or title of the lesson
    /// </summary>
    public string? Topic { get; set; }

    /// <summary>
    /// Gets or sets the description or additional details about the lesson
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the related class-subject-teacher relationship entity
    /// </summary>
    /// <remarks>
    /// This is a navigation property for Entity Framework.
    /// It provides access to the class, subject, and teacher information for this lesson.
    /// </remarks>
    public ClassSubjectTeacher ClassSubjectTeacher { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of attachments associated with this lesson
    /// </summary>
    /// <remarks>
    /// This is a navigation property for Entity Framework.
    /// Attachments can include homework assignments, resources, or other materials related to the lesson.
    /// </remarks>
    public ICollection<Attachment> Attachments { get; set; } = [];
}