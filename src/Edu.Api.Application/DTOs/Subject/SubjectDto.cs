namespace Edu.Api.Application.DTOs.Subject;

/// <summary>
/// Data transfer object for basic subject information
/// </summary>
public class SubjectDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the subject
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the subject
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the subject
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the subject is currently active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the collection of teachers assigned to this subject
    /// </summary>
    public IEnumerable<SubjectTeacherDto> Teachers { get; set; } = [];
}