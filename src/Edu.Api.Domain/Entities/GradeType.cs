using Edu.Api.Domain.Interfaces;

namespace Edu.Api.Domain.Entities;

/// <summary>
/// Represents a type of grade or assessment in the educational system
/// </summary>
/// <remarks>
/// Grade types define different categories of assessments (such as exams, quizzes, homework)
/// and their relative importance through weighting factors
/// </remarks>
public class GradeType : IEntity<int>
{
    /// <summary>
    /// Gets or sets the unique identifier for the grade type
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the grade type
    /// </summary>
    /// <remarks>
    /// Examples include "Final Exam", "Quiz", "Homework", "Project", etc.
    /// </remarks>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the weight of this grade type for calculating overall averages
    /// </summary>
    /// <remarks>
    /// A higher weight indicates greater importance in the final grade calculation.
    /// For example, a final exam might have a weight of 0.4 (40%) while homework might have 0.1 (10%).
    /// </remarks>
    public float Weight { get; set; }

    /// <summary>
    /// Gets or sets the description of the grade type
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the collection of grades associated with this grade type
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public ICollection<Grade> Grades { get; set; } = [];
}