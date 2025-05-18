using Edu.Api.Domain.Interfaces;

namespace Edu.Api.Domain.Entities;

/// <summary>
/// Represents a class entity in an educational institution
/// </summary>
public class Class : IEntity<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier for the class
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the name of the class
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the academic year of the class
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Gets or sets the section identifier of the class
    /// </summary>
    public string? Section { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the teacher assigned as the class teacher
    /// </summary>
    public Guid ClassTeacherId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the class is currently active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the teacher assigned as the class teacher
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public User ClassTeacher { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of student-class relationships
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework representing students enrolled in this class</remarks>
    public ICollection<StudentClass> StudentClasses { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of class-subject-teacher relationships
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework representing subjects taught in this class</remarks>
    public ICollection<ClassSubjectTeacher> ClassSubjectTeacher { get; set; } = [];
}