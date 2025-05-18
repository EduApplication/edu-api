using Edu.Api.Domain.Interfaces;

namespace Edu.Api.Domain.Entities;

/// <summary>
/// Represents a relationship between a parent and a student
/// </summary>
/// <remarks>
/// This entity maps parent users to student users and defines their relationship type
/// </remarks>
public class ParentStudent : IEntity<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier for the parent-student relationship
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the identifier of the parent user
    /// </summary>
    public Guid ParentId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the student user
    /// </summary>
    public Guid StudentId { get; set; }

    /// <summary>
    /// Gets or sets the type of relationship between the parent and student
    /// </summary>
    /// <remarks>
    /// Examples include "Mother", "Father", "Guardian", "Stepparent", etc.
    /// </remarks>
    public string? RelationType { get; set; }

    /// <summary>
    /// Gets or sets the parent user
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public User Parent { get; set; } = null!;

    /// <summary>
    /// Gets or sets the student user
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public User Student { get; set; } = null!;
}