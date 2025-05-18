namespace Edu.Api.Application.DTOs.Class;

/// <summary>
/// Data transfer object for creating a new class
/// </summary>
public class CreateClassDto
{
    /// <summary>
    /// Name of the class to create
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Academic year of the class
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Section identifier of the class (e.g., A, B, C)
    /// </summary>
    public string? Section { get; set; }

    /// <summary>
    /// Unique identifier of the teacher assigned as the class teacher
    /// </summary>
    public Guid ClassTeacherId { get; set; }
}