namespace Edu.Api.Application.DTOs.Subject;

/// <summary>
/// Data transfer object for teacher information in the context of a subject
/// </summary>
public class SubjectTeacherDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the teacher
    /// </summary>
    public Guid TeacherId { get; set; }

    /// <summary>
    /// Gets or sets the first name of the teacher
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the teacher
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the teacher
    /// </summary>
    public string? Email { get; set; }
}