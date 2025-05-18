namespace Edu.Api.Application.DTOs.ClassSubjectTeacher;

/// <summary>
/// Represents a relationship between a class, subject, and teacher
/// </summary>
public class ClassSubjectTeacherDto
{
    /// <summary>
    /// Unique identifier of the class-subject-teacher relationship
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Identifier of the class in this relationship
    /// </summary>
    public Guid ClassId { get; set; }

    /// <summary>
    /// Name of the class in this relationship
    /// </summary>
    public string? ClassName { get; set; }

    /// <summary>
    /// Identifier of the subject in this relationship
    /// </summary>
    public Guid SubjectId { get; set; }

    /// <summary>
    /// Name of the subject in this relationship
    /// </summary>
    public string? SubjectName { get; set; }

    /// <summary>
    /// Identifier of the teacher in this relationship
    /// </summary>
    public Guid TeacherId { get; set; }

    /// <summary>
    /// First name of the teacher in this relationship
    /// </summary>
    public string? TeacherFirstName { get; set; }

    /// <summary>
    /// Last name of the teacher in this relationship
    /// </summary>
    public string? TeacherLastName { get; set; }
}