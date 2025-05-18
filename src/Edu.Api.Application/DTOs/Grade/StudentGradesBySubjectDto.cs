namespace Edu.Api.Application.DTOs.Grade;

/// <summary>
/// Representation of a student's grades grouped by subject with calculated averages
/// </summary>
public class StudentGradesBySubjectDto
{
    /// <summary>
    /// Unique identifier of the subject
    /// </summary>
    public Guid SubjectId { get; set; }

    /// <summary>
    /// Name of the subject
    /// </summary>
    public string? SubjectName { get; set; }

    /// <summary>
    /// Collection of individual grades for this subject
    /// </summary>
    public IEnumerable<GradeDto> Grades { get; set; } = [];

    /// <summary>
    /// Calculated average grade for this subject
    /// </summary>
    public float AverageGrade { get; set; }
}