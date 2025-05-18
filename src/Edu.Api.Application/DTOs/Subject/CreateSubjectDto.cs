namespace Edu.Api.Application.DTOs.Subject;

/// <summary>
/// Data transfer object for creating a new subject
/// </summary>
public class CreateSubjectDto
{
    /// <summary>
    /// Gets or sets the name of the subject
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the subject
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the collection of teacher identifiers to be assigned to this subject
    /// </summary>
    public IEnumerable<Guid> TeacherIds { get; set; } = [];
}