namespace Edu.Api.Application.DTOs.Grade;

/// <summary>
/// Data transfer object for creating a new grade for a student
/// </summary>
public class CreateGradeDto
{
    /// <summary>
    /// Unique identifier of the student receiving the grade
    /// </summary>
    public Guid StudentId { get; set; }

    /// <summary>
    /// Unique identifier of the subject for which the grade is assigned
    /// </summary>
    public Guid SubjectId { get; set; }

    /// <summary>
    /// Identifier of the grade type (e.g., exam, homework, project)
    /// </summary>
    public int GradeTypeId { get; set; }

    /// <summary>
    /// Numeric value of the grade
    /// </summary>
    public float Value { get; set; }

    /// <summary>
    /// Optional comment or feedback about the grade
    /// </summary>
    public string? Comment { get; set; }
}