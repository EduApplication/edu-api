namespace Edu.Api.Application.DTOs.Class;

/// <summary>
/// Basic representation of a class for list views
/// </summary>
public class ClassDto
{
    /// <summary>
    /// Unique identifier of the class
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the class
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
    /// First name of the class teacher
    /// </summary>
    public string? ClassTeacherFirstName { get; set; }

    /// <summary>
    /// Last name of the class teacher
    /// </summary>
    public string? ClassTeacherLastName { get; set; }

    /// <summary>
    /// Indicates whether the class is currently active
    /// </summary>
    public bool IsActive { get; set; }
}