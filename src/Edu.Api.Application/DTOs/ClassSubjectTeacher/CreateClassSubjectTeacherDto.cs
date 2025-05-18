namespace Edu.Api.Application.DTOs.ClassSubjectTeacher;

/// <summary>
/// Data transfer object for creating a new relationship between a class and a teacher-subject pair
/// </summary>
public class CreateClassSubjectTeacherDto
{
    /// <summary>
    /// Identifier of the class to include in the relationship
    /// </summary>
    public Guid ClassId { get; set; }

    /// <summary>
    /// Identifier of the teacher-subject relationship that connects a specific teacher with a subject
    /// </summary>
    public Guid TeacherSubjectId { get; set; }
}