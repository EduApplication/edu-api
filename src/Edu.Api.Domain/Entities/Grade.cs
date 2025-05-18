using Edu.Api.Domain.Interfaces;

namespace Edu.Api.Domain.Entities;

/// <summary>
/// Represents a grade (mark) assigned to a student for a specific subject
/// </summary>
public class Grade : IEntity<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier for the grade
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the identifier of the student who received the grade
    /// </summary>
    public Guid StudentId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the subject for which the grade was assigned
    /// </summary>
    public Guid SubjectId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the teacher who assigned the grade
    /// </summary>
    public Guid TeacherId { get; set; }

    /// <summary>
    /// Gets or sets the numerical value of the grade
    /// </summary>
    public float Value { get; set; }

    /// <summary>
    /// Gets or sets an optional comment about the grade
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the grade was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the grade type
    /// </summary>
    /// <remarks>
    /// Refers to the type of assessment (e.g., exam, quiz, homework)
    /// </remarks>
    public int GradeTypeId { get; set; }

    /// <summary>
    /// Gets or sets the student who received the grade
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public User Student { get; set; } = null!;

    /// <summary>
    /// Gets or sets the subject for which the grade was assigned
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public Subject Subject { get; set; } = null!;

    /// <summary>
    /// Gets or sets the teacher who assigned the grade
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public User Teacher { get; set; } = null!;

    /// <summary>
    /// Gets or sets the type of the grade
    /// </summary>
    /// <remarks>
    /// This is a navigation property for Entity Framework.
    /// The grade type defines the category and weight of the assessment.
    /// </remarks>
    public GradeType GradeType { get; set; } = null!;
}