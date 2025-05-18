using Edu.Api.Domain.Interfaces;

namespace Edu.Api.Domain.Entities;

/// <summary>
/// Represents a relationship between a teacher and a subject
/// </summary>
public class TeacherSubject : IEntity<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier for the teacher-subject relationship
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the identifier of the teacher
    /// </summary>
    public Guid TeacherId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the subject
    /// </summary>
    public Guid SubjectId { get; set; }

    /// <summary>
    /// Gets or sets the teacher entity
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public User Teacher { get; set; } = null!;

    /// <summary>
    /// Gets or sets the subject entity
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public Subject Subject { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of class-subject-teacher relationships
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework that links this teacher-subject pair to specific classes</remarks>
    public ICollection<ClassSubjectTeacher> ClassSubjectTeacher { get; set; } = [];
}