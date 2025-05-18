namespace Edu.Api.Application.DTOs.Grade;

/// <summary>
/// Basic representation of a grade for list views
/// </summary>
public class GradeDto
{
    /// <summary>
    /// Unique identifier of the grade
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Numeric value of the grade
    /// </summary>
    public float Value { get; set; }

    /// <summary>
    /// Name of the subject for which the grade was assigned
    /// </summary>
    public string? SubjectName { get; set; }
}