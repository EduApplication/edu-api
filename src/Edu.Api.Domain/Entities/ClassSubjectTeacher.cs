using Edu.Api.Domain.Interfaces;

namespace Edu.Api.Domain.Entities;

/// <summary>
/// Represents a relationship between a class, subject, and teacher
/// </summary>
/// <remarks>
/// This entity serves as a junction table connecting classes to teacher-subject assignments,
/// enabling the tracking of which teachers teach which subjects in specific classes
/// </remarks>
public class ClassSubjectTeacher : IEntity<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier for the class-subject-teacher relationship
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the identifier of the class
    /// </summary>
    public Guid ClassId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the teacher-subject relationship
    /// </summary>
    public Guid TeacherSubjectId { get; set; }

    /// <summary>
    /// Gets or sets the related class entity
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public Class Class { get; set; } = null!;

    /// <summary>
    /// Gets or sets the related teacher-subject relationship entity
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public TeacherSubject TeacherSubject { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of lessons associated with this class-subject-teacher relationship
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework representing all lessons for this specific class, subject and teacher combination</remarks>
    public ICollection<Lesson> Lessons { get; set; } = [];
}