namespace Edu.Api.Application.DTOs.Grade;

/// <summary>
/// Detailed representation of a grade with associated student, subject, and teacher information
/// </summary>
public class GradeDetailsDto
{
    /// <summary>
    /// Unique identifier of the grade
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Identifier of the student who received the grade
    /// </summary>
    public Guid StudentId { get; set; }

    /// <summary>
    /// First name of the student
    /// </summary>
    public string? StudentFirstName { get; set; }

    /// <summary>
    /// Last name of the student
    /// </summary>
    public string? StudentLastName { get; set; }

    /// <summary>
    /// Email address of the student
    /// </summary>
    public string? StudentEmail { get; set; }

    /// <summary>
    /// Identifier of the subject for which the grade was assigned
    /// </summary>
    public Guid SubjectId { get; set; }

    /// <summary>
    /// Name of the subject
    /// </summary>
    public string? SubjectName { get; set; }

    /// <summary>
    /// Identifier of the teacher who assigned the grade
    /// </summary>
    public Guid TeacherId { get; set; }

    /// <summary>
    /// First name of the teacher
    /// </summary>
    public string? TeacherFirstName { get; set; }

    /// <summary>
    /// Last name of the teacher
    /// </summary>
    public string? TeacherLastName { get; set; }

    /// <summary>
    /// Email address of the teacher
    /// </summary>
    public string? TeacherEmail { get; set; }

    /// <summary>
    /// Numeric value of the grade
    /// </summary>
    public float Value { get; set; }

    /// <summary>
    /// Optional comment or feedback provided with the grade
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Date and time when the grade was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Identifier of the grade type
    /// </summary>
    public int GradeTypeId { get; set; }

    /// <summary>
    /// Name of the grade type (e.g., Exam, Homework, Project)
    /// </summary>
    public string? GradeTypeName { get; set; }

    /// <summary>
    /// Description of the grade type
    /// </summary>
    public string? GradeTypeDescription { get; set; }
}