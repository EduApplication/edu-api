using Edu.Api.Domain.Interfaces;

namespace Edu.Api.Domain.Entities;

/// <summary>
/// Represents a relationship between a student and a class
/// </summary>
public class StudentClass : IEntity<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier for the student-class relationship
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the identifier of the student
    /// </summary>
    public Guid StudentId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the class
    /// </summary>
    public Guid ClassId { get; set; }

    /// <summary>
    /// Gets or sets the date when the student joined the class
    /// </summary>
    public DateTime JoinDate { get; set; }

    /// <summary>
    /// Gets or sets the student entity
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public User Student { get; set; } = null!;

    /// <summary>
    /// Gets or sets the class entity
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public Class Class { get; set; } = null!;
}