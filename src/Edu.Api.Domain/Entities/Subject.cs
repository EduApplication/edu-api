using Edu.Api.Domain.Interfaces;

namespace Edu.Api.Domain.Entities;

/// <summary>
/// Represents an academic subject or course
/// </summary>
public class Subject : IEntity<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier for the subject
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the name of the subject
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the subject
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the subject is currently active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the collection of teacher-subject relationships
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework that links teachers to this subject</remarks>
    public ICollection<TeacherSubject> TeacherSubjects { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of grades assigned for this subject
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public ICollection<Grade> Grades { get; set; } = [];
}